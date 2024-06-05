using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using MyFirstProject;
using Repositories;

namespace TestProject
{
    public class UserRepositoryUnitTest
    {
        [Fact]
        public async  Task GetUser_ValidCredentials_ReturnUser()
        {
            var user = new User { Email = "yaeli8748@gmail.com", Password = "password", FirstName = "yael", LastName = "Iluz" };
            var mockContext = new Mock<Market326354982Context>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepositories(mockContext.Object);

            var result = await userRepository.Login(user);
            Assert.Equal(user, result);
        }
        [Fact]
        public async Task Register_NewUser_ReturnsUser()
        {
            var user = new User { Email = "yaeli8748@gmail.com", Password = "password", FirstName = "yael", LastName = "Iluz" };

            var mockContext = new Mock<Market326354982Context>();
            var mockDbSet = new Mock<DbSet<User>>();
            mockContext.Setup(c => c.Users).Returns(mockDbSet.Object);

            var userRepository = new UserRepositories(mockContext.Object);

            var result = await userRepository.Register(user);

           /* mockDbSet.Verify(m => m.AddAsync(user, It.IsAny<CancellationToken>()), Times.Once());
            mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());*/

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task UpdateUser_ExistingUser_ReturnsUpdatedUser()
        {
            var user = new User {  Email = "yaeli8748@gmail.com", Password = "password", FirstName = "yael", LastName = "Iluz" };
            var updatedUser = new User {  Email = "yaeli8748@gmail.com", Password = "newpassword", FirstName = "UpdatedYael", LastName = "UpdatedIluz" };

            var mockContext = new Mock<Market326354982Context>();
            var mockDbSet = new Mock<DbSet<User>>();

            mockDbSet.Setup(m => m.Update(It.IsAny<User>())).Callback<User>(u => user = u);
            mockContext.Setup(c => c.Users).Returns(mockDbSet.Object);

            var userRepository = new UserRepositories(mockContext.Object);

            var result = await userRepository.UpdateUser(updatedUser, 1);

            Assert.Equal(updatedUser, result);
        }
        [Fact]
        
        public async Task GetById_ValidId_ReturnsUser()
        {
            var user = new User { UserId = 1, Email = "yaeli8748@gmail.com", Password = "password", FirstName = "yael", LastName = "Iluz" };

            var mockContext = new Mock<Market326354982Context>();
            var mockDbSet = new Mock<DbSet<User>>();

            // Mock the FindAsync method
            mockDbSet.Setup(m => m.FindAsync(It.IsAny<int>()))
                     .ReturnsAsync(user);

            mockContext.Setup(c => c.Users).Returns(mockDbSet.Object);

            var userRepository = new UserRepositories(mockContext.Object);

            var result = await userRepository.GetById(1);

            Assert.Equal(user, result);
        }

    }
}