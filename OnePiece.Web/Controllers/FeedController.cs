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
    public class FeedController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public FeedController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> List(ListRequest rq)
        {
            var feeds = await _dbContext.Feeds
                .Include(x => x.Photos)
                .Include(x => x.Videos)
                .Skip(rq.Skip).Take(rq.Take).ToListAsync();

            return Json(feeds);
        }
    }
}
