using MyFirstProject;

namespace Services
{
    public interface IUserService
    {
        int CheckPassword(string password);
        Task<User> GetById(int id);
        Task<User> Login(User user);
        Task<User> Register(User user);
        Task<User> UpdateUser(User user, int id);
    }
}