using AutoMapper;
using JAP_Task_1_MoviesApi.Data;
using JAP_Task_1_MoviesApi.DTO;
using JAP_Task_1_MoviesApi.Models;
using JAP_Task_1_MoviesApi.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerData)
        {
            var response = await _authService.Register(new User 
                { Username = registerData.Username, FirstName = registerData.FirstName, LastName = registerData.LastName }, 
                registerData.Password);

            return (response.Success) ? Ok(response) : BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDataDto loginData)
        {
            var response = await _authService.Login(loginData.Username, loginData.Password);

            return (response.Success) ? Ok(response) : BadRequest(response);
        }

    }
}
