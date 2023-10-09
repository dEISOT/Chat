using AutoMapper;
using ChatCore.DTO;
using ChatCore.Mapping.AutoMapper;
using ChatCore.Services;
using ChatCore.Services.Interfaces;
using ChatData.Contexts;
using ChatData.Entities;
using ChatData.Repositories;
using ChatData.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public class UserServiceIntegrationTests : IDisposable
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly SqlConnection _sqlConnection;

    public UserServiceIntegrationTests()
    {
        var connectionString = "Server=tcp:localhost; Database=ChatDBTest; User Id=DESKTOP-5QGM80E\\edani; Password=password; Trust Server Certificate=True;";
        _sqlConnection = new SqlConnection(connectionString);
        _sqlConnection.Open();

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(_sqlConnection)
            .Options;



        _dbContext.Users.AddRange(new List<User> {  
                    new User{Id = new Guid("2f48bed2-c5ba-48c9-aca3-1639f75ada10"), Name = "TestUser1"},
                    new User{Id = new Guid("a6b022e2-53e0-4dfe-943a-73cb99ebd5ec"), Name = "TestUser2"},
                    new User{Id = new Guid("5331d2f7-2913-499b-abcf-2ebc004e7431"), Name = "TestUser3"},
                    });
        _dbContext.SaveChanges();

        _userRepository = new UserRepository(_dbContext);
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()));

        _userService = new UserService(_userRepository, _mapper);
    }

    [Fact]
    public async Task AddUserAsync_Should_Add_User_To_Database()
    {
        // Arrange
        var userDTO = new UserDTO { Name = "TestUser1" };

        // Act
        var userId = await _userService.AddUserAsync(userDTO);

        // Assert
        var user = await _dbContext.Users.FindAsync(userId);
        Assert.NotNull(user);
        Assert.Equal(userDTO.Name, user.Name);
        // Другие проверки на соответствие данным пользователями
    }

    [Fact]
    public async Task GetAllUsersAsync_Should_Return_All_Users_From_Database()
    {
        // Arrange
        

        // Act
        var users = await _userService.GetAllUsersAsync();

        // Assert
        Assert.NotNull(users);
        Assert.True(users.Any());
        // Другие проверки на соответствие данным пользователей
    }

    public void Dispose()
    {
        // Закройте соединение с SQL Server и очистите тестовую базу данных после завершения тестов
        _sqlConnection.Close();
        _sqlConnection.Dispose();
        _dbContext.Dispose();
    }
}
