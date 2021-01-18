using Movies.Api.Models;

namespace Movies.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public User GetUser(int id);
    }
}
