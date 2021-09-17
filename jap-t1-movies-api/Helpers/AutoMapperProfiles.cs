using AutoMapper;
using JAP_Task_1_MoviesApi.DTO;
using JAP_Task_1_MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<Movie, MovieDto>();
        }
    }
}
