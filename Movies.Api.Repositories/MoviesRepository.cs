using Movies.Api.DataAccess;
using Movies.Api.Models;
using Movies.Api.Repositories.Interfaces;
using System.Linq;

namespace Movies.Api.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        public MoviesDbContext _context;
        public MoviesRepository(MoviesDbContext context)
        {
            this._context = context;
        }
        public Movie GetMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            return new Movie()
            {
                Id = movie.Id,
                Title = movie.Title,
                runningTime = movie.runningTime,
                YearOfRelease = movie.YearOfRelease,
                Genres = movie.Genres.Split(',')
            };
        }
        public IQueryable<Movie> ListMovies()
        {
            var movies = _context.Movies;

            return movies.Select(x =>
               new Movie
               {
                   Id = x.Id,
                   Title = x.Title,
                   runningTime = x.runningTime,
                   YearOfRelease = x.YearOfRelease,
                   Genres = GenresSplit(x.Genres)
               }
             );
        }

        private static string[] GenresSplit(string generes)
        {
            return generes.Split(',');
        }
    }
}
