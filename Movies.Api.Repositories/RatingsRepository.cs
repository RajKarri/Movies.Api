using Movies.Api.DataAccess;
using Movies.Api.Models;
using Movies.Api.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Api.Repositories
{
    public class RatingsRepository : IRatingsRepository
    {
        public MoviesDbContext _context;
        public RatingsRepository(MoviesDbContext context)
        {
            this._context = context;
        }
        public Rating AddOrUpdateRating(Rating rating)
        {
            if (!this._context.Movies.Any(x =>x.Id == rating.MovieId) || !this._context.Users.Any(x => x.Id == rating.UserId))
            {
                throw new System.InvalidOperationException("Movie/User not found");
            }
            var existingRating = this._context.Ratings.FirstOrDefault(x => x.MovieId == rating.MovieId && x.UserId == rating.UserId);
            if (existingRating != null)
            {
                existingRating.Stars = rating.Stars;
                this._context.Ratings.Update(existingRating);
            }
            else
            {
                this._context.Ratings.Add(new Models.DbModels.Rating() { MovieId = rating.MovieId, UserId = rating.UserId, Stars = rating.Stars });
            }
            this._context.SaveChanges();
            var newRating = this._context.Ratings.FirstOrDefault(x => x.MovieId == rating.MovieId && x.UserId == rating.UserId);
            return new Rating() { Id = newRating.Id, MovieId = newRating.MovieId, UserId = newRating.UserId, Stars = newRating.Stars };
        }
        public IQueryable<Rating> ListRatings()
        {
            var ratings = _context.Ratings;
            IList<Rating> rts = new List<Rating>();

            return ratings.Select(x =>
                 new Rating()
                 {
                     Id = x.Id,
                     MovieId = x.MovieId,
                     UserId = x.UserId,
                     Stars = x.Stars
                 });
        }
    }
}
