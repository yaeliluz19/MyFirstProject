using MyFirstProject;

namespace Repositories
{
    public interface IUserRepositories
    {
        Task<User> GetById(int id);
        Task<User> Login(User user);
        Task<User> Register(User user);
        Task<User> UpdateUser(User user, int id);
    }
}