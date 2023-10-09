using AutoMapper;
using ChatCore.DTO;
using ChatCore.Services;
using ChatData.Entities;
using ChatData.Repositories.Interfaces;
using Moq;

namespace ChatTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;


        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

        }

        [Fact]
        public async Task AddUserAsync_Should_Add_User()
        {
            // Arrange
            var userDTO = new UserDTO { Name = "TestUser" };
            var userId = Guid.NewGuid();

            _userRepositoryMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
                .ReturnsAsync(userId);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<User>(userDTO))
                .Returns(new User());

            var userService = new UserService(_userRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await userService.AddUserAsync(userDTO);

            // Assert
            Assert.Equal(userId, result);
            _userRepositoryMock.Verify(repo => repo.AddUserAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task GetAllUsersAsync_Should_Return_All_Users()
        {
            // Arrange
            var users = new List<User> { new User { Name = "TestUser1"} , new User { Name = "TestUser2" } };

            _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync())
                .ReturnsAsync(users);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserDTO>>(users))
                .Returns(new List<UserDTO>());

            var userService = new UserService(_userRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await userService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(repo => repo.GetAllUsersAsync(), Times.Once);
        }
    }
}
