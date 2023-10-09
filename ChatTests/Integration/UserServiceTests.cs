using ChatCore.DTO;
using ChatCore.Services.Interfaces;
using ChatTests.Integration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

public class UserServiceIntegrationTests : IClassFixture<ApiWebApplicationFactory>
{
    private readonly ApiWebApplicationFactory _factory;
    private readonly HttpClient _httpClient;

    public UserServiceIntegrationTests(ApiWebApplicationFactory factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task GetAllUsersAsync_ReturnsOkResponse()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var userService = services.GetRequiredService<IUserService>();

            // Act 
            var users = await userService.GetAllUsersAsync();

            // Assert
            Assert.NotNull(users); 
            Assert.IsType<List<UserDTO>>(users); 
        }
    }

   
}


