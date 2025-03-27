using finefin.api.Data;
using finefin.api.Models.Entities;
using finefin.test._03._Builders.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace finefin.test._02._IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private LocalUser _user = default!;
        private string _password = string.Empty;

        public string GetEmail() => _user.Email;
        public string GetPassword() => _password;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                    if (descriptor is not null)
                        services.Remove(descriptor);

                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });

                    using var scope = services.BuildServiceProvider().CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    dbContext.Database.EnsureDeleted();

                    InsertData(dbContext);
                });
        }

        private void InsertData(AppDbContext context)
        {
            (_user, _password) = UserBuilder.Build();

            context.Users.Add(_user);
            context.SaveChanges();
        }

  
    }
}
