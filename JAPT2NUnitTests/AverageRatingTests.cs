using AutoMapper;
using JAP_Task_1_MoviesApi.Data;
using JAP_Task_1_MoviesApi.Helpers;
using JAP_Task_1_MoviesApi.Models;
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
    public class AverageRatingTests
    {
        ApplicationDbContext _context;
        IMovieService _movieService;
        IMapper _mapper;

        [SetUp]
        public async Task OneTimeSetupAsync()
        {
            // database setup
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "temp_moviesapp2")
                .Options;

            _context = new ApplicationDbContext(options);

            // - add data

            _context.Movies.Add(new Movie
            {
                Id = 1,
                Title = "The Shawshank Redemption",
                Overview = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                PosterPath = "https://swank.azureedge.net/swank/prod-film/3560cd8a-9491-4ab9-876c-8a8d6b84a6dd/f8e7c904-669a-4c9f-ac29-d19b64b43e33/one-sheet.jpg?width=335&height=508&mode=crop",
                Type = 0,
                ReleaseDate = new DateTime(1994, 9, 22),
                Actors = new List<Actor>
                {
                    new Actor { Id = 1, Name = "Morgan", Surname = "Freeman" },
                    new Actor { Id = 2, Name = "Bob", Surname = "Gunton" },
                },
                Ratings = new List<Rating>
                {
                    new Rating { Id = 1, Value = 4.6, MovieId = 1, UserId = 1 },
                    new Rating { Id = 2, Value = 3.6, MovieId = 1, UserId = 1 },
                    new Rating { Id = 3, Value = 4.1, MovieId = 1, UserId = 1 }
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
                Actors = new List<Actor>
                {
                    new Actor { Id = 3, Name = "Morgan", Surname = "Freeman" },
                    new Actor { Id = 4, Name = "Bob", Surname = "Gunton" }
                },
                Ratings = new List<Rating>
                {
                    new Rating { Id = 4, Value = 4.4, MovieId = 2, UserId = 1 }
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
                Actors = new List<Actor>
                {
                    new Actor { Id = 5, Name = "Morgan", Surname = "Freeman" },
                    new Actor { Id = 6, Name = "Bob", Surname = "Gunton" }
                },
                Ratings = new List<Rating>()
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
        public async Task RatingTest_NormalCase_ReturnsValid()
        {
            var videosList = (await _movieService.GetMoviesOrTvShows(0, new()));

            var film1 = videosList.Find(x => x.Id == 1);
            Assert.AreEqual(4.10, film1.AverageRating, .1);
        }

        [Test]
        public async Task RatingTest_NoRatings_Returns0()
        {
            var videosList = (await _movieService.GetMoviesOrTvShows(0, new()));

            var film1 = videosList.Find(x => x.Id == 3);
            Assert.AreEqual(0, film1.AverageRating);
        }

        [Test]
        public async Task RatingTest_RangeCheck_ReturnsNumberPositiveOrZero()
        {
            var videosList = (await _movieService.GetMoviesOrTvShows(0, new()));

            // every average rating needs to be between 0 and 5 (0 and 5 are included)
            videosList.ForEach(x =>
            {
                Assert.IsTrue(x.AverageRating >= 0 && x.AverageRating <= 5);
            });

        }
    }
}
