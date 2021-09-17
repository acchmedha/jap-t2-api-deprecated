using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JAP_Task_1_MoviesApi.Models;
using Microsoft.AspNetCore.Authorization;
using JAP_Task_1_MoviesApi.Helpers;
using JAP_Task_1_MoviesApi.Extensions;
using JAP_Task_1_MoviesApi.Services.MovieService;
using JAP_Task_1_MoviesApi.DTO;

namespace JAP_Task_1_MoviesApi.Controllers
{
    public class MoviesTvShowsController : BaseApiController
    {
        private readonly IMovieService _movieService;

        public MoviesTvShowsController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/MoviesTvShows/movies
        [HttpGet("movies")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovies([FromQuery] PaginationDto pagination)
        {
            var movies = await _movieService.GetMoviesOrTvShows(0, pagination);
            return Ok(movies);
        }

        // GET: api/MoviesTvShows/tvshows
        [HttpGet("tv-shows")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetTvShows([FromQuery] PaginationDto pagination, string search = null)
        {
            if (search == null) return Ok(await _movieService.GetMoviesOrTvShows(1, pagination));
            return Ok(await _movieService.GetFilteredMovies(search));
        }

        // GET: api/MoviesTvShows
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<MovieDto>>> GetFilteredMovies([FromQuery] PaginationDto pagination, string search = null, int type = 0)
        {
            if (search == null) return Ok(await _movieService.GetMoviesOrTvShows(type, pagination));
            return Ok(await _movieService.GetFilteredMovies(search));
        }



        // GET: api/MoviesTvShows/id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<MovieFullInfoDto>> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/MoviesTvShows/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            try
            {
                await _movieService.UpdateMovieAsync(movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_movieService.MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Movies updated");
        }

    }
}
