
namespace BookInspector
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using BookInspector.DATA;
    using BookInspector.SERVICES.Json;
    using BookInspector.SERVICES;

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();          

       //     var books = _jsonBooksImporterService.ImportBooks(@"https://www.googleapis.com/books/v1/volumes?q=object+c&maxResults=1", true);     
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

