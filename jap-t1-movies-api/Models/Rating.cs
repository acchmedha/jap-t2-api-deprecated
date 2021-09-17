using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
    }
}
