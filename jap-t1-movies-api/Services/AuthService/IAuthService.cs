using JAP_Task_1_MoviesApi.DTO;
using JAP_Task_1_MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<LoginDto>> Login(string username, string password);
    }
}
