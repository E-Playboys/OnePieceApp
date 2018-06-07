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
    public class NewsFeedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsFeedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NewsFeed
        public async Task<IActionResult> Index()
        {
            return View(await _context.Feeds.ToListAsync());
        }

        // GET: NewsFeed/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsFeed = await _context.Feeds
                .SingleOrDefaultAsync(m => m.Id == id);
            if (newsFeed == null)
            {
                return NotFound();
            }

            return View(newsFeed);
        }

        // GET: NewsFeed/Create
        public IActionResult Create()
        {
            ViewData["LinkToSelectList"] = LinkTo.Anime.ToSelectList(LinkTo.Anime);
            return View();
        }

        // POST: NewsFeed/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,TitleEng,Description,DescriptionEng,ColumnCount,LinkTo,LinkToId,Id")] NewsFeed newsFeed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsFeed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["LinkToSelectList"] = LinkTo.Anime.ToSelectList(newsFeed.LinkTo);
            return View(newsFeed);
        }

        // GET: NewsFeed/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsFeed = await _context.Feeds.SingleOrDefaultAsync(m => m.Id == id);
            if (newsFeed == null)
            {
                return NotFound();
            }

            ViewData["LinkToSelectList"] = LinkTo.Anime.ToSelectList(newsFeed.LinkTo);
            return View(newsFeed);
        }

        // POST: NewsFeed/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,TitleEng,Description,DescriptionEng,ColumnCount,LinkTo,LinkToId,Id")] NewsFeed newsFeed)
        {
            if (id != newsFeed.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsFeed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsFeedExists(newsFeed.Id))
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

            ViewData["LinkToSelectList"] = LinkTo.Anime.ToSelectList(newsFeed.LinkTo);
            return View(newsFeed);
        }

        // GET: NewsFeed/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsFeed = await _context.Feeds
                .SingleOrDefaultAsync(m => m.Id == id);
            if (newsFeed == null)
            {
                return NotFound();
            }

            return View(newsFeed);
        }

        // POST: NewsFeed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsFeed = await _context.Feeds.SingleOrDefaultAsync(m => m.Id == id);
            _context.Feeds.Remove(newsFeed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsFeedExists(int id)
        {
            return _context.Feeds.Any(e => e.Id == id);
        }
    }
}
