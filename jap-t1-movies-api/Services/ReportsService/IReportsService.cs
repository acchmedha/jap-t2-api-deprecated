using JAP_Task_1_MoviesApi.DTO.Reports;
using JAP_Task_1_MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Services.ReportsService
{
    public interface IReportsService
    {
        Task<ServiceResponse<List<MostRatedMoviesReport>>> MostRatedMoviesReport();
        Task<ServiceResponse<List<MoviesWithMostScreeningsReport>>> MoviesWithMostScreeningsReport(DateIntervalDto dateInterval);
        Task<ServiceResponse<List<MoviesWithMostSoldTicketsReport>>> MoviesWithMostSoldTicketsReport();
    }
}
