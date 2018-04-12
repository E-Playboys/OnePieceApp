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
    public class MangaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MangaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mangas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Manga.Include(m => m.Season);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Mangas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manga = await _context.Manga
                .Include(m => m.Season)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }

        // GET: Mangas/Create
        public IActionResult Create()
        {
            ViewData["SeasonSelectList"] = new SelectList(_context.Seasons, "Id", "SeasonNumber");
            ViewData["MangaTypeSelectList"] = MangaType.BlackWhite.ToSelectList(MangaType.BlackWhite);
            return View();
        }

        // POST: Mangas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manga manga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeasonSelectList"] = new SelectList(_context.Seasons, "Id", "SeasonNumber", manga.SeasonId);
            ViewData["MangaTypeSelectList"] = MangaType.BlackWhite.ToSelectList(MangaType.BlackWhite);
            return View(manga);
        }

        // GET: Mangas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manga = await _context.Manga.SingleOrDefaultAsync(m => m.Id == id);
            if (manga == null)
            {
                return NotFound();
            }
            ViewData["SeasonSelectList"] = new SelectList(_context.Seasons, "Id", "SeasonNumber", manga.SeasonId);
            ViewData["MangaTypeSelectList"] = MangaType.BlackWhite.ToSelectList(MangaType.BlackWhite);
            return View(manga);
        }

        // POST: Mangas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Manga manga)
        {
            if (id != manga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MangaExists(manga.Id))
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
            ViewData["SeasonSelectList"] = new SelectList(_context.Seasons, "Id", "SeasonNumber", manga.SeasonId);
            ViewData["MangaTypeSelectList"] = MangaType.BlackWhite.ToSelectList(MangaType.BlackWhite);
            return View(manga);
        }

        // GET: Mangas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manga = await _context.Manga
                .Include(m => m.Season)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (manga == null)
            {
                return NotFound();
            }

            return View(manga);
        }

        // POST: Mangas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manga = await _context.Manga.SingleOrDefaultAsync(m => m.Id == id);
            _context.Manga.Remove(manga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MangaExists(int id)
        {
            return _context.Manga.Any(e => e.Id == id);
        }
    }
}
