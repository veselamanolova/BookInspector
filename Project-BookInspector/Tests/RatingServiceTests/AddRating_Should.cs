namespace Tests.RatingServiceTests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BookInspector.Services;
    using BookInspector.Data.Models; 
    using BookInspector.Data.Context;
    using Microsoft.EntityFrameworkCore; 

    [TestClass]
    public class AddRating_Should
    {
        public const string userName = "User1";
        public const string title = "TestTitle";

        [TestMethod]
        public void Suceed_WhenUserExists()
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName: "Suceed_WhenUserExists")
                .Options; 

            BookInspectorContext context = new BookInspectorContext(options);
            context.User.Add(new User()
            {
                UserId = 1,
                Name = userName
            });

            context.Book.Add(new Book()
            {
                BookId =1,
                Title = title
            });

            context.SaveChanges(); 

            var sut = new RatingService(context);

            var rating = sut.AddRating("TestTitle", "User1", 4);

            Assert.AreEqual(4, rating.Rating);
            Assert.AreEqual(title, rating.Book.Title);
            Assert.AreEqual(userName, rating.User.Name);
        }

    }
}
