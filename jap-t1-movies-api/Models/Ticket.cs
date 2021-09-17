using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ScreeningId { get; set; }
        public int UserId { get; set; }
        public int BoughtTickets { get; set; }
    }
}
