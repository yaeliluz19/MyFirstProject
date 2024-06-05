using MyFirstProject;
using Repositories;
using Zxcvbn;

namespace Services
{
    public class UserService : IUserService
    {
        private IUserRepositories _UserRepositories;
        public UserService(IUserRepositories UserRepository)
        {
            _UserRepositories = UserRepository;
        }

        //UserRepositories UserRepositories = new UserRepositories();
        public async Task<User> GetById(int id)
        {
            return await _UserRepositories.GetById(id);
        }
        public async Task<User> Register(User user)
        {
            if(CheckPassword(user.Password) > 1)
                return await _UserRepositories.Register(user);
            return null;
        }
        public async Task<User> Login(User user)
        {

            return  await _UserRepositories.Login(user);
        }
        public async Task<User> UpdateUser(User user, int id)
        {
           if (CheckPassword(user.Password) > 1)
            return await _UserRepositories.UpdateUser(user, id);
           return null;
        }
        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;
        }
    }
}
