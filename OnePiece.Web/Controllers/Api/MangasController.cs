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
            var manga = await _dbContext.Manga.Where(x => x.Id == id).Include(x => x.Medias).FirstOrDefaultAsync();

            return Json(manga);
        }
    }
}
