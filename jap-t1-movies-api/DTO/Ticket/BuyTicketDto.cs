using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.DTO.Ticket
{
    public class BuyTicketDto
    {
        public int ScreeningId { get; set; }
        public int NumberOfTickets { get; set; }
    }
}
