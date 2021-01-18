using Microsoft.AspNetCore.Mvc;
using Movies.Api.BusinessLogic.Interfaces;
using Movies.Api.Models;
using System;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        IMoviesRatingsLogic _moviesRatingsLogic;        
       
        /// <summary>
        /// Ratings controller constructor
        /// </summary>
        /// <param name="moviesRatingsLogic">Movie ratings logic</param>
        public RatingsController(IMoviesRatingsLogic moviesRatingsLogic)
        {
            this._moviesRatingsLogic = moviesRatingsLogic;
        }
        
        /// <summary>
        /// Add or update a rating
        /// </summary>
        /// <param name="rating">Rating</param>
        /// <returns>Rating</returns>
        [HttpPut]
        [Route("AddOrUpdateRating")]
        public ActionResult<Rating> AddOrUpdateRating(Rating rating)
        {
            try
            {
                if (rating.Stars < 1 || rating.Stars > 5 || rating.MovieId <= 0 || rating.UserId <= 0)
                {
                    return BadRequest("Invalid input");
                }

                return this._moviesRatingsLogic.AddOrUpdateMovieRating(rating);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return BadRequest("Invalid input");
            }
        }
    }
}
