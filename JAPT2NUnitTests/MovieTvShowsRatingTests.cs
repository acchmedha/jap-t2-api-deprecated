using JAP_Task_1_MoviesApi.Data;
using JAP_Task_1_MoviesApi.Models;
using JAP_Task_1_MoviesApi.Services.AuthService;
using JAP_Task_1_MoviesApi.Services.RatingService;
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

namespace JAP_Task_1_MoviesApi
{
    [TestFixture]
    class MovieTvShowsRatingTests
    {
        ApplicationDbContext _context;
        IRatingService _ratingService;
        IAuthService _authService;

        [SetUp]
        public async Task SetupAsync()
        {
            // database setup
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "temp_moviesapp4")
                .Options;

            _context = new ApplicationDbContext(options);

            // - add data

            AuthService.CreatePasswordHash("admin", out byte[] passHashAdmin, out byte[] passSaltAdmin);
            _context.Users.Add(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    FirstName = "Admin",
                    LastName = "Admin",
                    PasswordHash = passHashAdmin,
                    PasswordSalt = passSaltAdmin
                }
            );

            AuthService.CreatePasswordHash("user", out byte[] passHashUser, out byte[] passSaltUser);
            _context.Users.Add(
                new User
                {
                    Id = 2,
                    Username = "user",
                    FirstName = "User",
                    LastName = "User",
                    PasswordHash = passHashUser,
                    PasswordSalt = passSaltUser
                }
            );

            _context.Movies.Add(new Movie
            {
                Id = 1,
                Title = "The Shawshank Redemption",
                Overview = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                PosterPath = "https://swank.azureedge.net/swank/prod-film/3560cd8a-9491-4ab9-876c-8a8d6b84a6dd/f8e7c904-669a-4c9f-ac29-d19b64b43e33/one-sheet.jpg?width=335&height=508&mode=crop",
                Type = 0,
                ReleaseDate = new DateTime(1994, 9, 22),
                Ratings = new List<Rating>
                {
                    new Rating { Id = 1, Value = 4.6, MovieId = 1, UserId = 1 },
                    new Rating { Id = 2, Value = 4, MovieId = 1, UserId = 1 }
                }
            });
            _context.Movies.Add(new Movie
            {
                Id = 2,
                Title = "The Godfather",
                Overview = "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son.",
                PosterPath = "https://www.reelviews.net/resources/img/posters/thumbs/godfather_poster.jpg",
                Type = 0,
                ReleaseDate = new DateTime(1972, 3, 24),
                Ratings = new List<Rating>
                {
                    new Rating { Id = 3, Value = 2.6, MovieId = 2, UserId = 1 }
                }
            });
            _context.Movies.Add(new Movie
            {
                Id = 3,
                Title = "The Godfather: Part II",
                Overview = "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.",
                PosterPath = "https://shotonwhat.com/images/0071562-med.jpg",
                Type = 0,
                ReleaseDate = new DateTime(1974, 12, 20),
                Ratings = new List<Rating>
                {
                    new Rating { Id = 4, Value = 3.6, MovieId = 3, UserId = 1 }
                }
            });

            await _context.SaveChangesAsync();

            // --------------


            // IConfiguration setup
            var inMemoryConfigurationSettings = new Dictionary<string, string>
            {
               {"AppSettings:Token", "6673d1b96a3661ae9a2f9a04243291d8f181e016a877358658d16627d5677e33aa85d672a75b0286508ac8c94cbc27e49eddf38abcf88d3027c98a981c25fc62"}
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemoryConfigurationSettings)
                .Build();
            // --------------------


            //auth service setup
            _authService = new AuthService(_context, configuration);

            // login to get token and setup the httpContext, so it can be passed for the ticketsService
            var userLogin = await _authService.Login("user", "user");

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(x => x.HttpContext.Request.Headers.Add("Authorization", @"Bearer $userLogin.Result.Data.Token"));

            // - add claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, "2"),
                new Claim(ClaimTypes.Name, "user")
            };
            var identity = new ClaimsIdentity(claims, "User");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var context = new DefaultHttpContext
            {
                User = claimsPrincipal
            };

            context.Request.Headers["Authorization"] = "Bearer " + userLogin.Data.Token;
            mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);

            _ratingService = new RatingService(_context, mockHttpContextAccessor.Object);
            // ----------------------------------------------------------------------------------------

        }

        [TearDown]
        public async Task TearDownAsync()
        {
            await _context.Database.EnsureDeletedAsync();
        }

        [Test]
        public async Task AddRatingTest_InputValidRatingAdd_ReturnTrue()
        {
            var response = await _ratingService.AddRating(4.1, 2);

            //Is the rating added?
            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Message, "Successfully added rating");

            var ratingAfter = (await _context.Ratings.Where(x => x.MovieId == 1).ToListAsync()).Average(x => x.Value);

            Assert.AreEqual(4.3, ratingAfter, .1);
        }

        [Test]
        public async Task AddRatingTest_InputInValidRatingAdd_ReturnFalse()
        {
            var response1 = await _ratingService.AddRating(4.1, 2);
            var response2 = await _ratingService.AddRating(4.1, 2);

            //Is the rating added?

            //first one needs to be added
            Assert.IsTrue(response1.Success);
            Assert.AreEqual(response1.Message, "Successfully added rating");

            //second time it shouldn't. one user cannot rate the same film/show twice!
            Assert.IsFalse(response2.Success);
            Assert.AreEqual(response2.Message, "You already rated this item");

            var ratingAfter = (await _context.Ratings.Where(x => x.MovieId == 1).ToListAsync()).Average(x => x.Value);

            Assert.AreEqual(4.3F, ratingAfter, .1);
        }
    }
}
