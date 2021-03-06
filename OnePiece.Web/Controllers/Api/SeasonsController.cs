﻿using System;
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
    public class SeasonsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SeasonsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List(ListRequest rq)
        {
            var seasons = await _dbContext.Seasons.OrderByDescending(x => x.Id).Skip(rq.Skip).Take(rq.Take).ToListAsync();
            return Json(seasons);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var season = await _dbContext.Seasons.Where(x => x.Id == id)
                .Include(x => x.Episodes)
                .Include(x => x.Chapters)
                .FirstOrDefaultAsync();

            return Json(season);
        }
    }
}
