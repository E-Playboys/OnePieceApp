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
    public class VolumeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolumeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Volume
        public async Task<IActionResult> Index()
        {
            return View(await _context.Volumes.ToListAsync());
        }

        // GET: Volume/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volume = await _context.Volumes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (volume == null)
            {
                return NotFound();
            }

            return View(volume);
        }

        // GET: Volume/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Volume/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VolumeNumber,Title,TitleEng,Description,DescriptionEng,Poster,ChapterRange,Id")] Volume volume)
        {
            if (ModelState.IsValid)
            {
                _context.Add(volume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(volume);
        }

        // GET: Volume/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volume = await _context.Volumes.SingleOrDefaultAsync(m => m.Id == id);
            if (volume == null)
            {
                return NotFound();
            }
            return View(volume);
        }

        // POST: Volume/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VolumeNumber,Title,TitleEng,Description,DescriptionEng,Poster,ChapterRange,Id")] Volume volume)
        {
            if (id != volume.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(volume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolumeExists(volume.Id))
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
            return View(volume);
        }

        // GET: Volume/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volume = await _context.Volumes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (volume == null)
            {
                return NotFound();
            }

            return View(volume);
        }

        // POST: Volume/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var volume = await _context.Volumes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Volumes.Remove(volume);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolumeExists(int id)
        {
            return _context.Volumes.Any(e => e.Id == id);
        }
    }
}
