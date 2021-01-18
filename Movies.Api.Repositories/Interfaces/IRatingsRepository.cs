using Movies.Api.Models;
using System.Linq;

namespace Movies.Api.Repositories.Interfaces
{
    public interface IRatingsRepository
    {
        public Rating AddOrUpdateRating(Rating rating);
        public IQueryable<Rating> ListRatings();
    }
}
