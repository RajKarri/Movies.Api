using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Movies.Api.BusinessLogic;
using Movies.Api.BusinessLogic.Interfaces;
using Movies.Api.Models;
using Movies.Api.Repositories;
using Movies.Api.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Api.Tests
{
    [TestClass]
    public class MoviesRatingsLogicTests
    {
        IMoviesRatingsLogic _moviesRatingsLogic;
        List<Movie> movies = new List<Movie>();
        List<Rating> ratings = new List<Rating>();

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();
            
            Mock<IMoviesRepository> mockMoviesRepository = new Mock<IMoviesRepository>();
            Mock<IRatingsRepository> mockRatingsRepository = new Mock<IRatingsRepository>();

            movies.AddRange(new Movie[]{
                new Movie {Id = 1, Title = "Titanic", runningTime = "1:40", YearOfRelease = 1920, Genres = new List<string>{"Fiction, Thriller" } },
                new Movie {Id = 2, Title = "Avatar", runningTime = "2:40", YearOfRelease = 1955, Genres = new List<string>{"SciFi, Thriller" } },
                new Movie {Id = 3, Title = "Terminator", runningTime = "1:55", YearOfRelease = 1944, Genres = new List<string>{"Comedy" } },
                new Movie {Id = 4, Title = "Iron man", runningTime = "2:45", YearOfRelease = 1922, Genres = new List<string>{"Action" } },
                new Movie {Id = 5, Title = "Alien", runningTime = "2:30", YearOfRelease = 1995, Genres = new List<string>{"Mythology" } },
                new Movie {Id = 6, Title = "Aliens", runningTime = "2:11", YearOfRelease = 2006, Genres = new List<string>{"Horror" } },
            });

            ratings.AddRange(new Rating[]{
                new Rating { Id = 1, MovieId = 1, UserId = 1, Stars = 1},
                new Rating { Id = 2, MovieId = 1, UserId = 2, Stars = 4},
                new Rating { Id = 3, MovieId = 1, UserId = 3, Stars = 5},

                new Rating { Id = 4, MovieId = 2, UserId = 1, Stars = 1},
                new Rating { Id = 5, MovieId = 2, UserId = 2, Stars = 5},
                new Rating { Id = 6, MovieId = 2, UserId = 3, Stars = 5},

                new Rating { Id = 7, MovieId = 3, UserId = 1, Stars = 1},
                new Rating { Id = 8, MovieId = 3, UserId = 2, Stars = 3},
                new Rating { Id = 9, MovieId = 3, UserId = 3, Stars = 2},

                new Rating { Id = 10, MovieId = 4, UserId = 1, Stars = 5},
                new Rating { Id = 11, MovieId = 4, UserId = 2, Stars = 4},
                new Rating { Id = 12, MovieId = 4, UserId = 3, Stars = 4},

                new Rating { Id = 13, MovieId = 5, UserId = 1, Stars = 5},
                new Rating { Id = 14, MovieId = 5, UserId = 2, Stars = 5},
                new Rating { Id = 15, MovieId = 5, UserId = 3, Stars = 5},

                new Rating { Id = 16, MovieId = 6, UserId = 1, Stars = 2},
                new Rating { Id = 17, MovieId = 6, UserId = 2, Stars = 4},
            }.ToList());

            mockMoviesRepository.Setup(x => x.ListMovies()).Returns(movies.AsQueryable());
            mockMoviesRepository.Setup(x => x.GetMovie(It.IsAny<int>())).Returns<int>(id => movies.FirstOrDefault(x => x.Id == id));
            mockRatingsRepository.Setup(x => x.ListRatings()).Returns(ratings.AsQueryable());

            _moviesRatingsLogic = new MoviesRatingsLogic(mockMoviesRepository.Object, mockRatingsRepository.Object);
        }

        [TestMethod]
        public void TopFiveMoviesByRating_MovieTitleVerification()
        {
           var response = _moviesRatingsLogic.TopFiveMoviesByRating();
           Assert.AreEqual("Alien", response[0].Title);
           Assert.AreEqual("Iron man", response[1].Title);
           Assert.AreEqual("Avatar", response[2].Title);
           Assert.AreEqual("Titanic", response[3].Title);
           Assert.AreEqual("Terminator", response[4].Title);
        }

        [TestMethod]
        public void Test_TopFiveMoviesBySpecificUser_TopRatedMovieVerification()
        {
            var response = _moviesRatingsLogic.TopFiveMoviesBySpecificUser(1);
            Assert.AreEqual("Iron man", response.First().Title);
        }
    }
}
