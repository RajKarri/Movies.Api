using Movies.Api.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Movies.Api.DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(MoviesDbContext context)
        {
            List<Movie> movies = new List<Movie>();
            List<User> users = new List<User>();
            List<Rating> ratings = new List<Rating>();

            if (context.Movies.Any())
            {
                return;
            }

            movies.AddRange(new Movie[]{
                new Movie {Title = "Titanic", runningTime = "1:40", YearOfRelease = 1920, Genres = "Fiction, Thriller"},
                new Movie {Title = "Avatar", runningTime = "2:40", YearOfRelease = 1955, Genres = "SciFi, Thriller" },
                new Movie {Title = "Terminator", runningTime = "1:55", YearOfRelease = 1944, Genres = "Comedy" },
                new Movie {Title = "Iron man", runningTime = "2:45", YearOfRelease = 1922, Genres = "Action" },
                new Movie {Title = "Alien", runningTime = "2:30", YearOfRelease = 1995, Genres = "Mythology" },
                new Movie {Title = "Aliens", runningTime = "2:11", YearOfRelease = 2006, Genres = "Horror" },
            }.ToList());
            context.Movies.AddRange(movies);
            context.SaveChanges();

            users.AddRange(new User[]{
                new User { Name = "David"},
                new User { Name = "John" },
                new User { Name = "Robert" },
            }.ToList());
            context.Users.AddRange(users);
            context.SaveChanges();

            ratings.AddRange(new Rating[]{
                new Rating { MovieId = 1, UserId = 1, Stars = 1},
                new Rating { MovieId = 1, UserId = 2, Stars = 4},
                new Rating { MovieId = 1, UserId = 3, Stars = 5},

                new Rating { MovieId = 2, UserId = 1, Stars = 1},
                new Rating { MovieId = 2, UserId = 2, Stars = 5},
                new Rating { MovieId = 2, UserId = 3, Stars = 5},

                new Rating { MovieId = 3, UserId = 1, Stars = 1},
                new Rating { MovieId = 3, UserId = 2, Stars = 3},
                new Rating { MovieId = 3, UserId = 3, Stars = 2},

                new Rating { MovieId = 4, UserId = 1, Stars = 5},
                new Rating { MovieId = 4, UserId = 2, Stars = 4},
                new Rating { MovieId = 4, UserId = 3, Stars = 4},

                new Rating { MovieId = 5, UserId = 1, Stars = 5},
                new Rating { MovieId = 5, UserId = 2, Stars = 5},
                new Rating { MovieId = 5, UserId = 3, Stars = 5},

                new Rating { MovieId = 6, UserId = 1, Stars = 2},
                new Rating { MovieId = 6, UserId = 2, Stars = 4},
            }.ToList());
            context.Ratings.AddRange(ratings);
            context.SaveChanges();
        }
    }
}
