using Movies.Api.Models;
using Movies.Api.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Api.BusinessLogic.Interfaces
{
    public interface IMoviesRatingsLogic
    {
        public IList<MovieRatingModel> SearchMovies(SearchModel searchModel);
        public IList<MovieRatingModel> TopFiveMoviesByRating();
        public IList<MovieRatingModel> TopFiveMoviesBySpecificUser(int userId);
        public Rating AddOrUpdateMovieRating(Rating rating);
    }
}
