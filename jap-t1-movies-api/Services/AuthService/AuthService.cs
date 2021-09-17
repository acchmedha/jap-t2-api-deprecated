using JAP_Task_1_MoviesApi.Data;
using JAP_Task_1_MoviesApi.DTO;
using JAP_Task_1_MoviesApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Services.AuthService
{
    public class AuthService : IAuthService
    {

        private readonly SymmetricSecurityKey _key;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public AuthService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<ServiceResponse<LoginDto>> Login(string username, string password)
        {
            ServiceResponse<LoginDto> response = new();
            User user = null;

            try
            {
                user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Internal server error";
                return response;
            }

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password";
            }
            else
            {
                LoginDto userLogin = new();
                userLogin.Token = CreateToken(user);
                userLogin.Username = user.Username;
                userLogin.FirstName = user.FirstName;
                userLogin.LastName = user.LastName;

                response.Data = userLogin;
                response.Message = "Login successful!";
            }

            return response;

        }


        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>();

            try
            {
                if (await UserExists(user.Username))
                {
                    response.Success = false;
                    response.Message = "User already exists.";
                    return response;
                }

                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                response.Data = user.Id;
                response.Message = "Registered successfully!";
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Internal server error";
            }

            return response;
        }

        private async Task<bool> UserExists(string username) => await _context.Users.AnyAsync(x => x.Username == username.ToLower());



        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
                if (computedHash[i] != passwordHash[i]) return false;

            return true;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
