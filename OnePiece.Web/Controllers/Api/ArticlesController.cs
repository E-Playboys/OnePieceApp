using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePiece.Web.Data;
using OnePiece.Web.Data.Entities;
using OnePiece.Web.Models;

namespace OnePiece.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ArticlesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List(ListRequest rq)
        {
            var articles = await _dbContext.Articles.OrderByDescending(x => x.Id).Skip(rq.Skip).Take(rq.Take).ToListAsync();
            return Json(articles);
        }

        [HttpGet]
        [Route("TopFeatured")]
        public async Task<IActionResult> TopFeatured(ListRequest rq)
        {
            var articles = await _dbContext.Articles.Where(x => x.IsFeatured).OrderByDescending(x => x.Id).Skip(rq.Skip).Take(rq.Take).ToListAsync();
            return Json(articles);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await _dbContext.Articles.Where(x => x.Id == id).FirstOrDefaultAsync();

            return Json(article);
        }
    }
}
