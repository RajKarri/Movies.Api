using Movies.Api.DataAccess;
using Movies.Api.Models;
using Movies.Api.Repositories.Interfaces;
using System.Linq;

namespace Movies.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private MoviesDbContext _context;
        public UserRepository(MoviesDbContext context)
        {
            this._context = context;
        }
        public User GetUser(int id)
        {
           var user = this._context.Users.FirstOrDefault(x => x.Id == id);
            return new User() { Id = user.Id, Name = user.Name };
        }
    }
}
