using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePiece.Web.Data;
using OnePiece.Web.Models;

namespace OnePiece.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class MangasController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MangasController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List(ListRequest rq)
        {
            var mangas = await _dbContext.Manga
                .Skip(rq.Skip).Take(rq.Take).ToListAsync();

            return Json(mangas);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var manga = await _dbContext.Manga.Where(x => x.Id == id).Include(x => x.MangaPages).FirstOrDefaultAsync();

            return Json(manga);
        }

        [HttpGet]
        [Route("GetByChapterNumber")]
        public async Task<IActionResult> GetByChapterNumber(int chapterNumber, int next, int previous)
        {
            var query = _dbContext.Manga.Include(x => x.MangaPages).AsQueryable();

            if(next > 0)
            {
                query = query.Where(x => x.ChapterNumber > chapterNumber).OrderBy(x => x.ChapterNumber).Skip(next - 1);
            }
            else if(previous > 0)
            {
                query = query.Where(x => x.ChapterNumber < chapterNumber).OrderByDescending(x => x.ChapterNumber).Skip(previous - 1);
            }
            else
            {
                query = query.Where(x => x.ChapterNumber == chapterNumber);
            }

            var manga = await query.FirstOrDefaultAsync();

            return Json(manga);
        }
    }
}
