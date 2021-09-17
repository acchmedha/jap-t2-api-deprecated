using JAP_Task_1_MoviesApi.DTO.Ticket;
using JAP_Task_1_MoviesApi.Services.TicketService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Controllers
{
    [Authorize]
    public class TicketsController : BaseApiController
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("buy-tickets")]
        public async Task<IActionResult> BuyTickets([FromBody] BuyTicketDto buyTicketDto)
        {
            var response = await _ticketService.BuyTickets(buyTicketDto);

            return response.Success ? Ok(response) : BadRequest(response);

        }
    }
}