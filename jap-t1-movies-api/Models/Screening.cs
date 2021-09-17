using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Models
{
    public class Screening
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MovieId { get; set; }
        public int SoldTickets { get; set; }
        public DateTime ScreeningDate { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
