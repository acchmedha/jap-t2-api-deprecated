using JAP_Task_1_MoviesApi.DTO.Ticket;
using JAP_Task_1_MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Services.TicketService
{
    public interface ITicketService
    {
        Task<ServiceResponse<bool>> BuyTickets(BuyTicketDto buyTicketDto);
    }
}
