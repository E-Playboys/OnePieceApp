using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnePiece.Web.Data;
using OnePiece.Web.Data.Entities;

namespace OnePiece.Web.Controllers
{
    public class MangaPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MangaPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MangaPage
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MangaPages.Include(m => m.Manga);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MangaPage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mangaPage = await _context.MangaPages
                .Include(m => m.Manga)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mangaPage == null)
            {
                return NotFound();
            }

            return View(mangaPage);
        }

        // GET: MangaPage/Create
        public IActionResult Create()
        {
            ViewData["MangaId"] = new SelectList(_context.Manga, "Id", "Id");
            return View();
        }

        // POST: MangaPage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Url,Width,Height,PageNumber,MangaId,Id")] MangaPage mangaPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mangaPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MangaId"] = new SelectList(_context.Manga, "Id", "Id", mangaPage.MangaId);
            return View(mangaPage);
        }

        // GET: MangaPage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mangaPage = await _context.MangaPages.SingleOrDefaultAsync(m => m.Id == id);
            if (mangaPage == null)
            {
                return NotFound();
            }
            ViewData["MangaId"] = new SelectList(_context.Manga, "Id", "Id", mangaPage.MangaId);
            return View(mangaPage);
        }

        // POST: MangaPage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Url,Width,Height,PageNumber,MangaId,Id")] MangaPage mangaPage)
        {
            if (id != mangaPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mangaPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MangaPageExists(mangaPage.Id))
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
            ViewData["MangaId"] = new SelectList(_context.Manga, "Id", "Id", mangaPage.MangaId);
            return View(mangaPage);
        }

        // GET: MangaPage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mangaPage = await _context.MangaPages
                .Include(m => m.Manga)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mangaPage == null)
            {
                return NotFound();
            }

            return View(mangaPage);
        }

        // POST: MangaPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mangaPage = await _context.MangaPages.SingleOrDefaultAsync(m => m.Id == id);
            _context.MangaPages.Remove(mangaPage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MangaPageExists(int id)
        {
            return _context.MangaPages.Any(e => e.Id == id);
        }
    }
}
