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
    public class AnimeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Animes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Episodes.Include(a => a.Season);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Animes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anime = await _context.Episodes
                .Include(a => a.Season)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // GET: Animes/Create
        public IActionResult Create()
        {
            ViewData["SeasonSelectList"] = new SelectList(_context.Seasons, "Id", "SeasonNumber");
            ViewData["AnimeTypeSelectList"] = AnimeType.Movie.ToSelectList(AnimeType.Movie);
            return View();
        }

        // POST: Animes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EpisodeNumber,Title,TitleEng,Description,DescriptionEng,ImdbScore,Rating,ViewCount,Type,SeasonId,Id")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeasonSelectList"] = new SelectList(_context.Seasons, "Id", "SeasonNumber", anime.SeasonId);
            ViewData["AnimeTypeSelectList"] = AnimeType.Movie.ToSelectList(anime.Type);
            return View(anime);
        }

        // GET: Animes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anime = await _context.Episodes.SingleOrDefaultAsync(m => m.Id == id);
            if (anime == null)
            {
                return NotFound();
            }
            ViewData["SeasonSelectList"] = new SelectList(_context.Seasons, "Id", "SeasonNumber", anime.SeasonId);
            ViewData["AnimeTypeSelectList"] = AnimeType.Movie.ToSelectList(anime.Type);
            return View(anime);
        }

        // POST: Animes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EpisodeNumber,Title,TitleEng,Description,DescriptionEng,ImdbScore,Rating,ViewCount,Type,SeasonId,Id")] Anime anime)
        {
            if (id != anime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeExists(anime.Id))
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
            ViewData["SeasonSelectList"] = new SelectList(_context.Seasons, "Id", "SeasonNumber", anime.SeasonId);
            ViewData["AnimeTypeSelectList"] = AnimeType.Movie.ToSelectList(anime.Type);
            return View(anime);
        }

        // GET: Animes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anime = await _context.Episodes
                .Include(a => a.Season)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (anime == null)
            {
                return NotFound();
            }

            return View(anime);
        }

        // POST: Animes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anime = await _context.Episodes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Episodes.Remove(anime);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeExists(int id)
        {
            return _context.Episodes.Any(e => e.Id == id);
        }
    }
}
