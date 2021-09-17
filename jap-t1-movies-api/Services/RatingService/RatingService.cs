using JAP_Task_1_MoviesApi.Data;
using JAP_Task_1_MoviesApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Services.RatingService
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _httpContextAccessor;

        public RatingService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<bool>> AddRating(double AddValue, int AddMovieId)
        {
            ServiceResponse<bool> response = new();

            var AddUserId = GetUserId();
            var userAlreadyAddedRating = await _context.Ratings.FirstOrDefaultAsync(x => x.UserId == AddUserId && x.MovieId == AddMovieId);

            if (userAlreadyAddedRating != null)
            {
                response.Data = false;
                response.Success = false;
                response.Message = "You already rated this item";
                return response;
            }

            var addRating = new Rating
            {
                Value = AddValue,
                MovieId = AddMovieId,
                UserId = AddUserId
            };

            await _context.Ratings.AddAsync(addRating);
            await _context.SaveChangesAsync();

            response.Data = true;
            response.Success = true;
            response.Message = "Successfully added rating";

            return response;
        }
    }
}
