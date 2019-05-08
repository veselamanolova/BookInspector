
namespace BookInspector
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using BookInspector.DATA;
    using BookInspector.SERVICES.Json;
    using BookInspector.SERVICES;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using BookInspector.DATA.Models;
    using Microsoft.AspNetCore.Identity;

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            SeedDatabase(host);

            host.Run();
        }

        private static void SeedDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                SeedAdmin(scope);
            }
        }

        private static void SeedAdmin(IServiceScope scope)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext.Roles.Any(u => u.Name == "Admin"))
            {
                return;
            }
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Wait();

            var adminUser = new DbUser { UserName = "Admin", Email = "admin@admin.admin" };
            userManager.CreateAsync(adminUser, "Admin123@").Wait();

            userManager.AddToRoleAsync(adminUser, "Admin").Wait();
          
        }
    

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

