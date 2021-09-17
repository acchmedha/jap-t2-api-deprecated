using AutoMapper;
using JAP_Task_1_MoviesApi.Data;
using JAP_Task_1_MoviesApi.Helpers;
using JAP_Task_1_MoviesApi.Models;
using JAP_Task_1_MoviesApi.Services.AuthService;
using JAP_Task_1_MoviesApi.Services.MovieService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi
{
    [TestFixture]
    public class MoviesTvShowsSearchTests
    {
        ApplicationDbContext _context;
        IMovieService _movieService;
        IMapper _mapper;

        [SetUp]
        public async Task OneTimeSetupAsync()
        {
            // database setup
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "temp_moviesapp3")
                .Options;

            _context = new ApplicationDbContext(options);

            // - add data

            AuthService.CreatePasswordHash("admin", out byte[] passHash, out byte[] passSalt);
            _context.Users.Add(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    FirstName = "Admin",
                    LastName = "Admin",
                    PasswordSalt = passSalt,
                    PasswordHash = passHash
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

            IConfiguration configuration = new ConfigurationBuilder().Build();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            _mapper = mappingConfig.CreateMapper();

            _movieService = new MovieService(_context, _mapper);

        }

        [TearDown]
        public async Task TearDownAsync()
        {
            await _context.Database.EnsureDeletedAsync();
        }
        [Test]
        public async Task VideoSearchTests_InputNormalFilmTitle_ReturnListOf1()
        {
            var list = (await _movieService.GetFilteredMovies("The s"));

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(list[0].Title, "The Shawshank Redemption");
        }

        [Test]
        public async Task VideoSearchTests_InputAfter1970_ReturnListOf1()
        {
            var list = (await _movieService.GetFilteredMovies("after 1980"));

            //should only be one from the three given movies
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public async Task VideoSearchTests_InputAfter72EdgeCase_ReturnListOf2()
        {
            var list = (await _movieService.GetFilteredMovies("after 1972"));

            //movie with the release year 1972 should not be included
            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public async Task VideoSearchTests_InputDescriptionWord_ReturnListOf1()
        {
            var list = (await _movieService.GetFilteredMovies("Vito Corleone"));

            //should only be one from the three given movies
            Assert.AreEqual(1, list.Count);
            Assert.IsTrue(list[0].Overview.Contains("Vito Corleone"));
        }

        [Test]
        public async Task VideoSearchTests_InputExactRating_ReturnListOf1()
        {
            var list = (await _movieService.GetFilteredMovies("4.3 stars"));

            //should only be one from the three given movies
            Assert.AreEqual(0, list.Count);
        }

    }
}
