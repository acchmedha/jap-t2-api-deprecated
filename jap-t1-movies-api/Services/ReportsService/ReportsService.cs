using JAP_Task_1_MoviesApi.Data;
using JAP_Task_1_MoviesApi.DTO.Reports;
using JAP_Task_1_MoviesApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Services.ReportsService
{
    public class ReportsService : IReportsService
    {
        private readonly ApplicationDbContext _context;

        public ReportsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<MostRatedMoviesReport>>> MostRatedMoviesReport()
        {
            var serviceResponse = new ServiceResponse<List<MostRatedMoviesReport>>
            {
                Data = await _context.MostRatedMoviesReports.FromSqlRaw("EXEC [dbo].[getTop10MoviesWithMostRatings];")
                                                            .ToListAsync()
            };

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<MoviesWithMostScreeningsReport>>> MoviesWithMostScreeningsReport(DateIntervalDto dateInterval)
        {
            var serviceResponse = new ServiceResponse<List<MoviesWithMostScreeningsReport>>
            {
                Data = await _context.MoviesWithMostScreeningsReports
                     .FromSqlRaw("EXEC [dbo].[getTop10MoviesWithMostScreenings] {0}, {1};", dateInterval.StartDate, dateInterval.EndDate)
                     .ToListAsync()
            };

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<MoviesWithMostSoldTicketsReport>>> MoviesWithMostSoldTicketsReport()
        {
            var serviceResponse = new ServiceResponse<List<MoviesWithMostSoldTicketsReport>>
            {
                Data = await _context.MoviesWithMostSoldTicketsReports.FromSqlRaw("EXEC [dbo].[getMoviesWithMostSoldTicketsNoRating]")
                                                                      .ToListAsync()
            };

            return serviceResponse;
        }

    }
}
