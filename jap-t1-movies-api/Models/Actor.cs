using System.Collections.Generic;


namespace JAP_Task_1_MoviesApi.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
