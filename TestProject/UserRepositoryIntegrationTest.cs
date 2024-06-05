using MyFirstProject;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace TestProject
{
    public class UserRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly Market326354982Context _dbContext;
        private readonly UserRepositories _userRepository;

        public UserRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _userRepository = new UserRepositories(_dbContext);
        }
        [Fact]
        public async Task GetUser_ValidCredentials_ReturnsUser()
        {
            var email = "yaeli8748@gmail.com";
            var password = "password";
            var user = new User { Email = email, Password = password, FirstName = "yael", LastName = "Iluz" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            var result = await _userRepository.Login(user);
            Assert.NotNull(result);

        }
        [Fact]
        public async Task UpdateUser_ValidUser_UpdatesUser()
        {
            //Arrange
            var user = new User { Email = "updateuser@example.com", Password = "password", FirstName = "Eve", LastName = "Johnson" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var updatedUser = new User { Email = "updateduser@example.com", Password = "newpassword", FirstName = "UpdatedName", LastName = "UpdatedLastName" };
            _dbContext.Entry(user).State = EntityState.Detached;
            updatedUser.UserId = user.UserId;
            //Act
            var result = await _userRepository.UpdateUser(updatedUser, user.UserId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("updateduser@example.com", result.Email);
        }
        [Fact]
        public async Task RegisterUser_ValidUser_AddsUser()
        {
            //Arrange
            var user = new User { Email = "newuser@example.com", Password = "securepassword", FirstName = "John", LastName = "Doe" };

            //Act
            var result = await _userRepository.Register(user);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("newuser@example.com", result.Email);
        }
        [Fact]
        public async Task GetUserById_ValidId_ReturnsUser()
        {
            //Arrange
            var user = new User { Email = "userbyid@example.com", Password = "password", FirstName = "Alice", LastName = "Smith" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _userRepository.GetById(user.UserId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("userbyid@example.com", result.Email);
        }

    }
}
