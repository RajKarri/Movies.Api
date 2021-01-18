using Microsoft.AspNetCore.Mvc;
using Movies.Api.BusinessLogic.Interfaces;
using Movies.Api.Models;
using Movies.Api.Models.ApiModels;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        IMoviesRatingsLogic _moviesRatingsLogic;

        public MoviesController(IMoviesRatingsLogic moviesRatingsLogic)
        {
            this._moviesRatingsLogic = moviesRatingsLogic;
        }

        [HttpPost]
        [Route("SearchMovies")]
        public ActionResult<IList<MovieRatingModel>> SearchMovies(SearchModel searchModel)
        {
            if (string.IsNullOrEmpty(searchModel.Title) && searchModel.YearOfRelease <= 0 && searchModel.Genres.Count() < 1)
            {
                return BadRequest("Invalid input");
            }

            var movies = this._moviesRatingsLogic.SearchMovies(searchModel).ToList();
            return movies.Count() > 0 ? movies : NotFound("No movies found");
        }

        [HttpGet]
        [Route("TopFiveMoviesByRating")]
        public ActionResult<IList<MovieRatingModel>> TopFiveMoviesByRating()
        {
            var movies = this._moviesRatingsLogic.TopFiveMoviesByRating().ToList();
            return movies.Count() > 0 ? movies : NotFound("No movies found");
        }

        [HttpGet]
        [Route("TopFiveMoviesBySpecificUser")]
        public ActionResult<IList<MovieRatingModel>> TopFiveMoviesBySpecificUser(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid input");
            }

            var movies = this._moviesRatingsLogic.TopFiveMoviesBySpecificUser(userId).ToList();
            return movies.Count() > 0 ? movies : NotFound("No movies found");
        }
    }
}
