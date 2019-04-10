namespace Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public static class TestUtils
    {
        public static DbContextOptions GetOptions(string databaseName)
        {
            return  new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
