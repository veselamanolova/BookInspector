
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
    using BookInspector.SERVICES.Contracts;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    public class Program
    {

        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            SeedDatabaseAsync(host);

            host.Run();
        }

        private static async Task SeedDatabaseAsync(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                await SeedRoleAsync(scope, "Administrator");
                await SeedRoleAsync(scope, "User");
                await SeedAdminAsync(scope);
                SeedBooks(scope);
            }
        }

        private static async Task SeedRoleAsync(IServiceScope scope, string role)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext.Roles.Any(u => u.Name == role))
            {
                return;
            }
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole { Name = role });
        }

        private static async Task SeedAdminAsync(IServiceScope scope)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext.Users.Any(u => u.UserName == "Admin"))
            {
                return;
            }
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminUser = new ApplicationUser { UserName = "Admin", Email = "admin@admin.admin" };
            var createAdminUser = await userManager.CreateAsync(adminUser, "Admin123@");
            if (createAdminUser.Succeeded)
            {
                userManager.AddToRoleAsync(adminUser, "Administrator").Wait();
            }
        }


        private static void SeedBooks(IServiceScope scope)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext.Books.Any())
            {
                return;
            }
            var bookManager = scope.ServiceProvider.GetRequiredService<IJsonBooksImporterService> ();            
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
           
            var filePath =Path.Combine("Json", "booksSeed.json");            
            bookManager.ImportBooks(filePath, false);      
        }

       

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}