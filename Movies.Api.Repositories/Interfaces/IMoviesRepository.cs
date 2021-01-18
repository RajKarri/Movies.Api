using Movies.Api.Models;
using System.Linq;

namespace Movies.Api.Repositories.Interfaces
{
    public interface IMoviesRepository
    {
        public Movie GetMovie(int id);
        public IQueryable<Movie> ListMovies();
    }
}
