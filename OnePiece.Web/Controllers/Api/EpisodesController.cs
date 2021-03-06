﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnePiece.Web.Data;
using OnePiece.Web.Data.Entities;
using OnePiece.Web.Models;

namespace OnePiece.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class EpisodesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public EpisodesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var anime = await _dbContext.Episodes.Where(x => x.Id == id)
                .Include(x => x.Medias)
                .FirstOrDefaultAsync();

            return Json(anime);
        }

        [HttpGet]
        [Route("ListBySeason")]
        public async Task<IActionResult> ListBySeason(ListEpisodeBySeasonRequest rq)
        {
            var eps = await _dbContext.Episodes.Where(x => x.SeasonId == rq.SeasonId)
                .OrderByDescending(x => x.EpisodeNumber)
                .Include(x => x.Medias)
                .Skip(rq.Skip).Take(rq.Take)
                .ToListAsync();

            return Json(eps);
        }

        [HttpGet]
        [Route("ListStories")]
        public async Task<IActionResult> ListStories(ListRequest rq)
        {
            var eps = await _dbContext.Episodes.Where(x => x.Type == AnimeType.Story)
                .OrderByDescending(x => x.EpisodeNumber)
                .Include(x => x.Medias)
                .Skip(rq.Skip).Take(rq.Take).ToListAsync();

            return Json(eps);
        }

        [HttpGet]
        [Route("ListTvSpecials")]
        public async Task<IActionResult> ListTvSpecials(ListRequest rq)
        {
            var eps = await _dbContext.Episodes.Where(x => x.Type == AnimeType.TvSpecial)
                .OrderByDescending(x => x.EpisodeNumber)
                .Include(x => x.Medias)
                .Skip(rq.Skip).Take(rq.Take).ToListAsync();

            return Json(eps);
        }

        [HttpGet]
        [Route("ListMovies")]
        public async Task<IActionResult> ListMovies(ListRequest rq)
        {
            var eps = await _dbContext.Episodes.Where(x => x.Type == AnimeType.Movie)
                .OrderByDescending(x => x.EpisodeNumber)
                .Include(x => x.Medias)
                .Skip(rq.Skip).Take(rq.Take).ToListAsync();

            return Json(eps);
        }

        [HttpGet]
        [Route("GetLatestEpisode")]
        public async Task<IActionResult> GetLatestEpisode()
        {
            var ep = await _dbContext.Episodes.Include(x => x.Medias).Where(x => x.Type == AnimeType.Story).OrderByDescending(x => x.EpisodeNumber).FirstOrDefaultAsync();
            return Json(ep);
        }

        [HttpGet]
        [Route("GetLatestTvSpecial")]
        public async Task<IActionResult> GetLatestTvSpecial()
        {
            var ep = await _dbContext.Episodes.Include(x => x.Medias).Where(x => x.Type == AnimeType.TvSpecial).OrderByDescending(x => x.EpisodeNumber).FirstOrDefaultAsync();
            return Json(ep);
        }

        [HttpGet]
        [Route("GetLatestMovie")]
        public async Task<IActionResult> GetLatestMovie()
        {
            var ep = await _dbContext.Episodes.Include(x => x.Medias).Where(x => x.Type == AnimeType.Movie).OrderByDescending(x => x.EpisodeNumber).FirstOrDefaultAsync();
            return Json(ep);
        }
    }
}
