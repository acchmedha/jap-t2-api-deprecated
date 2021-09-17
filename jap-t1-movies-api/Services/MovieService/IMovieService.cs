using JAP_Task_1_MoviesApi.DTO;
using JAP_Task_1_MoviesApi.Helpers;
using JAP_Task_1_MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Services.MovieService
{
    public interface IMovieService
    {
        Task<List<MovieDto>> GetMoviesOrTvShows(int type, PaginationDto pagination);
        Task<MovieFullInfoDto> GetMovieById(int id);
        Task UpdateMovieAsync(Movie movie);
        Task<List<MovieDto>> GetFilteredMovies(string search);
        bool MovieExists(int id);
    }
}
