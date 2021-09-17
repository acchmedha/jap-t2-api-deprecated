using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PosterPath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Overview { get; set; }
        public short Type { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Screening> Screenings { get; set; }
    }
}
