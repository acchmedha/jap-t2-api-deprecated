using JAP_Task_1_MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Services.RatingService
{
    public interface IRatingService
    {
        Task<ServiceResponse<bool>> AddRating(double AddValue, int AddMovieId);
    }
}
