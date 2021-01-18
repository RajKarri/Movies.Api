using Movies.Api.BusinessLogic.Interfaces;
using Movies.Api.Models;
using Movies.Api.Models.ApiModels;
using Movies.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Api.BusinessLogic
{
    public class MoviesRatingsLogic : IMoviesRatingsLogic
    {
        private IMoviesRepository _moviesRepository;
        private IRatingsRepository _ratingsRepository;

        public MoviesRatingsLogic(IMoviesRepository moviesRepository,
            IRatingsRepository ratingsRepository)
        {
            this._moviesRepository = moviesRepository;
            this._ratingsRepository = ratingsRepository;
        }

        public Rating AddOrUpdateMovieRating(Rating rating)
        {
            return this._ratingsRepository.AddOrUpdateRating(rating);
        }

        public IList<MovieRatingModel> SearchMovies(SearchModel searchModel)
        {
            IQueryable<Movie> allMovies = _moviesRepository.ListMovies();
            IQueryable<Rating> ratings = _ratingsRepository.ListRatings();
            IList<MovieRatingModel> movieRatingModels = new List<MovieRatingModel>();

            var searchMovies = allMovies.AsEnumerable().Where(x =>
            (string.IsNullOrEmpty(searchModel.Title) || x.Title.ToLower().Contains(searchModel.Title.ToLower()))
            && (searchModel.YearOfRelease <= 0 || x.YearOfRelease == searchModel.YearOfRelease)
            && (searchModel.Genres.Count() <= 0 || searchModel.Genres.ToList().Any(y => x.Genres.Any(z => z.Contains(y))))).ToList();

            searchMovies.ForEach(x =>
            {
                movieRatingModels.Add(new MovieRatingModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    runningTime = x.runningTime,
                    YearOfRelease = x.YearOfRelease,
                    Genres = string.Join(",", x.Genres),
                    averageRating = RoundRating(GetOverallRating(ratings.Where(y => y.MovieId == x.Id).ToList()))
                });
            });

            return movieRatingModels;
        }

        public IList<MovieRatingModel> TopFiveMoviesByRating()
        {
            IQueryable<Movie> allMovies = _moviesRepository.ListMovies();
            IQueryable<Rating> ratings = _ratingsRepository.ListRatings();
            IList<MovieRatingModel> movieRatingModels = new List<MovieRatingModel>();
            var movieRatings = allMovies.Select(x => new { movieId = x.Id, rating = GetOverallRating(ratings.Where(y => y.MovieId == x.Id).ToList())});
            // Below line of code need to be improved as it doesn't yield results by rank.
            var top5Movies = movieRatings.ToList().Take(5);

            top5Movies.ToList().ForEach(x =>
            {
                var movie = _moviesRepository.GetMovie(x.movieId);
                var movieRating = movieRatings.Where(x => x.movieId == movie.Id).FirstOrDefault();

                movieRatingModels.Add(new MovieRatingModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    runningTime = movie.runningTime,
                    YearOfRelease = movie.YearOfRelease,
                    Genres = string.Join(",", movie.Genres),
                    averageRating = RoundRating(movieRating.rating)
                });
            });

            return movieRatingModels.OrderByDescending(x => x.averageRating).ThenByDescending(x => x.Title).ToList();
        }

        public IList<MovieRatingModel> TopFiveMoviesBySpecificUser(int userId)
        {
            IList<MovieRatingModel> movieRatingModels = new List<MovieRatingModel>();
            IQueryable<Rating> ratings = _ratingsRepository.ListRatings().Where(r => r.UserId == userId);
            IQueryable<Movie> allMovies = _moviesRepository.ListMovies().Where(x => ratings.Any(r => r.MovieId == x.Id));
            var movieRatings = allMovies.Select(x => new { movieId = x.Id, rating = GetOverallRating(ratings.Where(y => y.MovieId == x.Id).ToList())});
            // Below line of code need to be improved as it doesn't yield results by rank.
            var top5Movies = movieRatings.ToList().Take(5);

            top5Movies.ToList().ForEach(x =>
            {
                var movie = _moviesRepository.GetMovie(x.movieId);
                var movieRating = movieRatings.Where(x => x.movieId == movie.Id).FirstOrDefault();

                movieRatingModels.Add(new MovieRatingModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    runningTime = movie.runningTime,
                    YearOfRelease = movie.YearOfRelease,
                    Genres = string.Join(",", movie.Genres),
                    averageRating = RoundRating(movieRating.rating)
                });
            });

            return movieRatingModels.OrderByDescending(x => x.averageRating).ThenByDescending(x => x.Title).ToList();
        }

        private static double GetOverallRating(IList<Rating> ratings)
        {
            if (ratings.Count() > 0)
            {
                return ratings.Average(x => x.Stars);
            }

            return 0;
        }

        private static double RoundRating(double actualRating)
        {
            double avgRating = 0;

            if (actualRating > 0)
            {
                if (actualRating % 1 < 0.25)
                {
                    avgRating = Convert.ToInt32(actualRating / 1);
                }
                else if (actualRating % 1 >= 0.25 && actualRating % 1 < 0.74)
                {
                    avgRating = Convert.ToInt32(actualRating / 1) + 0.5;
                }
                else if (actualRating % 1 >= 0.75)
                {
                    avgRating = Convert.ToInt32(actualRating / 1) + 1;
                }
                return avgRating;
            }

            return avgRating;
        }
    }
}
