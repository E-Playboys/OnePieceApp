using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePiece.Web.Data;
using OnePiece.Web.Models;

namespace OnePiece.Web.Controllers
{
    public class SeasonController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SeasonController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> List(ListRequest rq)
        {
            var seasons = await _dbContext.Seasons
                .Include(x => x.Photos)
                .Skip(rq.Skip).Take(rq.Take).ToListAsync();

            return Json(seasons);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var season = await _dbContext.Seasons.Where(x => x.Id == id)
                .Include(x => x.Photos)
                .FirstOrDefaultAsync();

            return Json(season);
        }
    }
}
