using AutoMapper;
using JAP_Task_1_MoviesApi.Data;
using JAP_Task_1_MoviesApi.DTO;
using JAP_Task_1_MoviesApi.Helpers;
using JAP_Task_1_MoviesApi.Models;
using JAP_Task_1_MoviesApi.Services.MovieService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Services.MovieService
{
    public class MovieService : IMovieService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MovieService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       

        public async Task<MovieFullInfoDto> GetMovieById(int id)
        {
            var movie = await _context.Movies
                        .Include(x => x.Actors).AsSingleQuery()
                        .Include(x => x.Ratings).AsSingleQuery()
                        .Select(x => new MovieFullInfoDto
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Overview = x.Overview,
                            PosterPath = x.PosterPath,
                            ReleaseDate = x.ReleaseDate,
                            AverageRating = x.Ratings.Select(x => x.Value).DefaultIfEmpty().Average(),
                            Actors = x.Actors.Select(x => new ActorMovieDto { Name = x.Name, Surname = x.Surname }).ToList(),
                        })
                        .FirstOrDefaultAsync(x => x.Id == id);

            return movie;
        }

        public async Task<List<MovieDto>> GetMoviesOrTvShows(int type, PaginationDto pagination)
        {
            var query = await _context.Movies
                                   .Include(x => x.Ratings)
                                   .Where(x => x.Type == type)
                                   .Select(x => new MovieDto
                                   {
                                       Id = x.Id,
                                       Title = x.Title,
                                       Overview = x.Overview,
                                       PosterPath = x.PosterPath,
                                       ReleaseDate = x.ReleaseDate,
                                       AverageRating = x.Ratings.Select(x => x.Value)
                                                                             .DefaultIfEmpty()
                                                                             .Average()
                                   })
                                   .OrderByDescending(x => x.AverageRating)
                                   .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                   .Take(pagination.PageSize)
                                   .ToListAsync();

            return query;
        }

        public async Task<List<MovieDto>> GetFilteredMovies(string search)
        {
           List<MovieDto> data = new();
            var query = _context.Movies.AsQueryable();
            AddFiltersForMovieSearch(search, ref query);
            data = await query.OrderByDescending(x => x.Ratings.Select(x => x.Value)
                                                                .DefaultIfEmpty()
                                                                .Average())
                                                                .Select(x => _mapper.Map<MovieDto>(x))
                                                                .ToListAsync();

            return data;
        }
        private static void AddFiltersForMovieSearch(string Search, ref IQueryable<Movie> query)
        {

            var searchQuery = Regex.Split(Search, @"\s+").ToList();
            void setDefaultSearchQuery(ref IQueryable<Movie> q) => q = q.Where(x => x.Title.ToUpper().Contains(Search.ToUpper())
                                                                   || x.Overview.ToUpper().Contains(Search.ToUpper()));

            bool containingStringStar(string s) => s.ToUpper().Equals("STAR") || s.ToUpper().Equals("STARS");
            bool containingStringYear(string s) => s.ToUpper().Equals("YEAR") || s.ToUpper().Equals("YEARS");
            
            if (searchQuery.Count < 2)
            {
                setDefaultSearchQuery(ref query);
                return;
            }

            if (searchQuery.Count == 2)
            {
                if (searchQuery[0].ToUpper().Equals("AFTER") && int.TryParse(searchQuery[1], out int ratingForSearchAfter))
                    query = query.Where(x => x.ReleaseDate.Year > ratingForSearchAfter);
                else if (containingStringStar(searchQuery[1]) && float.TryParse(searchQuery[0], out float exactRating))
                    query = query.Where(x => x.Ratings.Select(x => x.Value).Average() == exactRating);
                else setDefaultSearchQuery(ref query);
            }
            else if (searchQuery.Count == 4)
            {
                if (searchQuery[0].ToUpper().Equals("AT") && searchQuery[1].ToUpper().Equals("LEAST")
                 && float.TryParse(searchQuery[2], out float ratingForSearchAtLeast)
                 && containingStringStar(searchQuery[3]))
                {
                    query = query.Where(x => x.Ratings.Select(x => x.Value).Average() >= ratingForSearchAtLeast);
                }
                else if (searchQuery[0].ToUpper().Equals("OLDER") && searchQuery[1].ToUpper().Equals("THAN")
                      && int.TryParse(searchQuery[2], out int dateForSearchOlderThan)
                      && containingStringYear(searchQuery[3]))
                {
                    query = query.Where(x => DateTime.Now.Year - x.ReleaseDate.Year > dateForSearchOlderThan);
                }
                else setDefaultSearchQuery(ref query);
            }
            else setDefaultSearchQuery(ref query);

        }


        public bool MovieExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
