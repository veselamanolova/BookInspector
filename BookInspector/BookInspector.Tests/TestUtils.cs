namespace BookInspector.Services.Tests.UserServiceTests
{
    using BookInspector.DATA;
    using Microsoft.EntityFrameworkCore;

    public static class TestUtils
    {
        public static DbContextOptions<ApplicationDbContext> GetOptions(string databaseName) => 
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
    }
}
