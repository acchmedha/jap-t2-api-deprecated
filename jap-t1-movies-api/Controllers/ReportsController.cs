using JAP_Task_1_MoviesApi.DTO.Reports;
using JAP_Task_1_MoviesApi.Models;
using JAP_Task_1_MoviesApi.Services.ReportsService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Controllers
{
    public class ReportsController : BaseApiController
    {
        private readonly IReportsService _reportsService;

        public ReportsController(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }

        [HttpGet("most-rated-movies")]
        public async Task<ActionResult<ServiceResponse<List<MostRatedMoviesReport>>>> GetMostRatedMoviesReport()
        {
            return Ok(await _reportsService.MostRatedMoviesReport());
        }

        [HttpPost("movies-with-most-screenings")]
        public async Task<ActionResult<ServiceResponse<List<MostRatedMoviesReport>>>> MoviesWithMostScreeningsReport([FromBody] DateIntervalDto dateInterval)
        {
            return Ok(await _reportsService.MoviesWithMostScreeningsReport(dateInterval));
        }

        [HttpGet("movies-with-most-sold-tickets")]
        public async Task<ActionResult<ServiceResponse<List<MoviesWithMostSoldTicketsReport>>>> MoviesWithMostSoldTicketsReport()
        {
            return Ok(await _reportsService.MoviesWithMostSoldTicketsReport());
        }

    }
}
