using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnePiece.Web.Data;
using OnePiece.Web.Data.Entities;
using OnePiece.Web.Utilities;

namespace OnePiece.Web.Controllers
{
    public class MediaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MediaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Media
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Medias.Include(m => m.Anime).Include(m => m.Manga).Include(m => m.NewsFeed);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Media/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Medias
                .Include(m => m.Anime)
                .Include(m => m.Manga)
                .Include(m => m.NewsFeed)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // GET: Media/Create
        public IActionResult Create()
        {
            ViewData["MediaTypeSelectList"] = MediaType.Gif.ToSelectList(MediaType.Gif);
            ViewData["MediaQualitySelectList"] = MediaQuality.P1080.ToSelectList(MediaQuality.P1080);
            ViewData["AnimeSelectList"] = new SelectList(_context.Episodes, "Id", "EpisodeNumber");
            ViewData["MangaSelectList"] = new SelectList(_context.Mangas, "Id", "ChapterNumber");
            ViewData["NewsFeedSelectList"] = new SelectList(_context.Feeds, "Id", "Title");
            return View();
        }

        // POST: Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Url,Width,Height,Type,Quality,MangaId,AnimeId,NewsFeedId,Id")] Media media)
        {
            if (ModelState.IsValid)
            {
                _context.Add(media);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MediaTypeSelectList"] = MediaType.Gif.ToSelectList(media.Type);
            ViewData["MediaQualitySelectList"] = MediaQuality.P1080.ToSelectList(media.Quality);
            ViewData["AnimeSelectList"] = new SelectList(_context.Episodes, "Id", "EpisodeNumber", media.AnimeId);
            ViewData["MangaSelectList"] = new SelectList(_context.Mangas, "Id", "ChapterNumber", media.MangaId);
            ViewData["NewsFeedSelectList"] = new SelectList(_context.Feeds, "Id", "Title", media.NewsFeedId);
            return View(media);
        }

        // GET: Media/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Medias.SingleOrDefaultAsync(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            ViewData["MediaTypeSelectList"] = MediaType.Gif.ToSelectList(media.Type);
            ViewData["MediaQualitySelectList"] = MediaQuality.P1080.ToSelectList(media.Quality);
            ViewData["AnimeSelectList"] = new SelectList(_context.Episodes, "Id", "EpisodeNumber", media.AnimeId);
            ViewData["MangaSelectList"] = new SelectList(_context.Mangas, "Id", "ChapterNumber", media.MangaId);
            ViewData["NewsFeedSelectList"] = new SelectList(_context.Feeds, "Id", "Title", media.NewsFeedId);
            return View(media);
        }

        // POST: Media/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Url,Width,Height,Type,Quality,MangaId,AnimeId,NewsFeedId,Id")] Media media)
        {
            if (id != media.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(media);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaExists(media.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["MediaTypeSelectList"] = MediaType.Gif.ToSelectList(media.Type);
            ViewData["MediaQualitySelectList"] = MediaQuality.P1080.ToSelectList(media.Quality);
            ViewData["AnimeSelectList"] = new SelectList(_context.Episodes, "Id", "EpisodeNumber", media.AnimeId);
            ViewData["MangaSelectList"] = new SelectList(_context.Mangas, "Id", "ChapterNumber", media.MangaId);
            ViewData["NewsFeedSelectList"] = new SelectList(_context.Feeds, "Id", "Title", media.NewsFeedId);
            return View(media);
        }

        // GET: Media/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Medias
                .Include(m => m.Anime)
                .Include(m => m.Manga)
                .Include(m => m.NewsFeed)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var media = await _context.Medias.SingleOrDefaultAsync(m => m.Id == id);
            _context.Medias.Remove(media);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaExists(int id)
        {
            return _context.Medias.Any(e => e.Id == id);
        }
    }
}
