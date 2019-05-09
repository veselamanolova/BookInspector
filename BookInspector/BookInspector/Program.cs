
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
                SeedBooks(scope);
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
            roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Admin"
            }); 

            userManager.AddToRoleAsync(adminUser, "Admin").Wait();
          
        }


        private static void SeedBooks(IServiceScope scope)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (dbContext.Books.Any())
            {
                return;
            }
            var bookManager = scope.ServiceProvider.GetRequiredService<IJsonBooksImporterService> ();
            //GetDirecoryInfo(); 
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
           
            var filePath =Path.Combine("Json", "booksSeed.json");            
            bookManager.ImportBooks(filePath, false);      
        }

        private static string GetDirecoryInfo(string filepath)
        {
            // DirectoryInfo d = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+ @"..\..\..\..\");//Assuming Test is your Folder
            DirectoryInfo d = new DirectoryInfo(filepath); 
             FileInfo[] Files = d.GetFiles(" * "); //Getting Text files
            string str = "";
            foreach (FileInfo file in Files)
            {
                str = str + ", " + file.Name;
            }

            return str; 
        }


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

