using Microsoft.EntityFrameworkCore;
using MyFirstProject;
using System.Runtime.InteropServices;
using System.Text.Json;


namespace Repositories
{
    public class UserRepositories : IUserRepositories
    {
        private Market326354982Context _market326354982;

        public UserRepositories(Market326354982Context market326354982Context)
        {
            _market326354982 = market326354982Context;
        }

        public async Task<User> GetById(int id)
        {
           return await _market326354982.Users.FindAsync(id);

        }
        public async Task<User> Register(User user)
        {
            try
            {
                await _market326354982.Users.AddAsync(user);
                await _market326354982.SaveChangesAsync();
                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> Login(User user)
        {
           return await _market326354982.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();
        }

        public async Task<User> UpdateUser(User user, int id)
        {
            user.UserId = id;
            _market326354982.Users.Update(user);
            await _market326354982.SaveChangesAsync();
            return user;
        }
    }
}
