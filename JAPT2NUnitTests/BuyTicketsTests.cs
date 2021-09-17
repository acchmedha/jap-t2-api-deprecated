using AutoMapper.Configuration;
using JAP_Task_1_MoviesApi.Controllers;
using JAP_Task_1_MoviesApi.Data;
using JAP_Task_1_MoviesApi.DTO.Ticket;
using JAP_Task_1_MoviesApi.Models;
using JAP_Task_1_MoviesApi.Services.AuthService;
using JAP_Task_1_MoviesApi.Services.TicketService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace JAP_Task_1_MoviesApi
{
    [TestFixture]
    public class BuyTicketsTests
    {
        ApplicationDbContext _context;
        IAuthService authService;
        ITicketService ticketService;

        [SetUp]
        public async Task OneTimeSetupAsync()
        {
            // database setup
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "temp_moviesapp1")
                .Options;

            _context = new ApplicationDbContext(options);

            // - add data
            _context.Screenings.Add(new Screening { Id = 1, Name = "Screening 1", MovieId = 1, ScreeningDate = DateTime.Now.AddDays(30) });
            _context.Screenings.Add(new Screening { Id = 2, Name = "Screening 2", MovieId = 1, ScreeningDate = DateTime.Now.AddDays(-3) });
            _context.Screenings.Add(new Screening { Id = 3, Name = "Screening 3", MovieId = 1, ScreeningDate = DateTime.Now.AddMinutes(30) });
            _context.Screenings.Add(new Screening { Id = 4, Name = "Screening 4", MovieId = 1, ScreeningDate = DateTime.Now.AddMinutes(30) });
            await _context.SaveChangesAsync();

            // IConfiguration setup
            var inMemoryConfigurationSettings = new Dictionary<string, string>
            {
               {"AppSettings:Token", "6673d1b96a3661ae9a2f9a04243291d8f181e016a877358658d16627d5677e33aa85d672a75b0286508ac8c94cbc27e49eddf38abcf88d3027c98a981c25fc62"}
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemoryConfigurationSettings)
                .Build();
            // --------------------//
            //auth service setup and add user (it doesnt matter that its the admin user)
            authService = new AuthService(_context, configuration);

            AuthService.CreatePasswordHash("admin", out byte[] passHash, out byte[] passSalt);
            _context.Users.Add(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    FirstName = "Admin",
                    LastName = "Admin",
                    PasswordHash = passHash,
                    PasswordSalt = passSalt
                }
            );
            await _context.SaveChangesAsync();
            // -------------------------------------------------------------------------
            // login to get token and setup the httpContext, so it can be passed for the ticketsService
            var userLogin = authService.Login("admin", "admin");
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(x => x.HttpContext.Request.Headers.Add("Authorization", @"Bearer $userLogin.Data.Token"));

            // - add claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "admin")
            };
            var identity = new ClaimsIdentity(claims, "User");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var context = new DefaultHttpContext
            {
                User = claimsPrincipal
            };

            context.Request.Headers["Authorization"] = "Bearer " + userLogin.Result.Data.Token;
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);

            ticketService = new TicketService(_context, mockHttpContextAccessor.Object);
            // ----------------------------------------------------------------------------------------

        }

        [TearDown]
        public async Task TearDownAsync()
        {
            await _context.Database.EnsureDeletedAsync();
        }

        [Test]
        public async Task BuyTicketsTest_ValidInput_ReturnsTrue()
        {
            var buyTicketDto = new BuyTicketDto { ScreeningId = 1, NumberOfTickets = 2 };

            // valid screening
            var response = await ticketService.BuyTickets(buyTicketDto);
            Assert.IsTrue(response.Data);
            Assert.AreEqual(response.Message, "Successfully bought tickets!");
        }

        [Test]
        public async Task BuyTicketsTest_ForScreeningInPast_ReturnsFalse()
        {
            var buyTicketDto = new BuyTicketDto { ScreeningId = 2, NumberOfTickets = 2 };

            var response = await ticketService.BuyTickets(buyTicketDto);

            // screening in past
            Assert.IsFalse(response.Data);
            Assert.AreEqual(response.Message, "Screening is in the past!");
        }    
    }
}
