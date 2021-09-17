using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.DTO
{
    public class MovieFullInfoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double AverageRating { get; set; }
        public List<ActorMovieDto> Actors { get; set; }
    }
}
