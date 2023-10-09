using ChatCore.Services.Interfaces;
using ChatCore.Services;
using ChatData.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.EntityFrameworkCore;
using ChatData.Repositories.Interfaces;
using ChatData.Repositories;

namespace ChatTests.Integration
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Program> 
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                // Здесь вы можете настроить конфигурацию, если это необходимо.
            });

            builder.ConfigureTestServices(services =>
            {

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();


                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddScoped<IUserRepository, UserRepository>();
            });
        }

       
    }
}
