namespace Tests.RatingServiceTests
{
    using System;    
    using BookInspector.Services;
    using BookInspector.Data.Models; 
    using BookInspector.Data.Context;    
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AddRating_Should
    {

        [TestMethod]
        public void Suceed_WhenUserAndBookExistsAndTheUserHasNotRatedTheBookBefore()
        {
            //var options = new DbContextOptionsBuilder()
            //    .UseInMemoryDatabase(databaseName: "Suceed_WhenUserExists")
            //    .Options;

            using (var arrangeContext = new BookInspectorContext(TestUtils.GetOptions(nameof(Suceed_WhenUserAndBookExistsAndTheUserHasNotRatedTheBookBefore))))
            {
                arrangeContext.User.Add(new User()
                {
                    UserId = 1,
                    Name = "User1"
                });

                arrangeContext.Book.Add(new Book()
                {
                    BookId = 1,
                    Title = "TestTitle"
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new BookInspectorContext(TestUtils.GetOptions(nameof(Suceed_WhenUserAndBookExistsAndTheUserHasNotRatedTheBookBefore))))
            {
                var sut = new RatingService(actAndAssertContext);

                var rating = sut.AddRating("TestTitle", "User1", 4);

                Assert.AreEqual(4, rating.Rating);
                Assert.AreEqual("TestTitle", rating.Book.Title);
                Assert.AreEqual("User1", rating.User.Name);
                Assert.AreEqual(1, rating.BookId);
                Assert.AreEqual(1, rating.UserId);
            }
        }


        [TestMethod]
        public void Fail_WhenUserDoesNotExist()
        {

            using (var arrangeContext = new BookInspectorContext(TestUtils.GetOptions(nameof(Fail_WhenUserDoesNotExist))))
            {

                arrangeContext.Book.Add(new Book()
                {
                    Title = "TestTitle"
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new BookInspectorContext(TestUtils.GetOptions(nameof(Fail_WhenUserDoesNotExist))))
            {
                var sut = new RatingService(actAndAssertContext);  
                
                var exception = Assert.ThrowsException<ArgumentException>(
                    () => sut.AddRating("TestTitle", "User1", 4),
                    "Expected Arguement exception not thrown");
                Assert.AreEqual($"The user does not exist!", exception.Message, "Arguement exception message is not correct");
            }
        }

        [TestMethod]
        public void Fail_WhenBookDoesNotExist()
        {

            using (var arrangeContext = new BookInspectorContext(TestUtils.GetOptions(nameof(Fail_WhenBookDoesNotExist))))
            {

                arrangeContext.User.Add(new User()
                {                    
                    Name = "User1"
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new BookInspectorContext(TestUtils.GetOptions(nameof(Fail_WhenBookDoesNotExist))))
            {
                var sut = new RatingService(actAndAssertContext);

                var exception = Assert.ThrowsException<ArgumentException>(
                    () => sut.AddRating("TestTitle", "User1", 4),
                    "Expected Arguement exception not thrown");
                Assert.AreEqual($"The book does not exist!", exception.Message, "Arguement exception message is not correct");
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(6)]
        public void Fail_WhenRatingIsNotInRange(int testRating)
        {

            using (var arrangeContext = new BookInspectorContext(TestUtils.GetOptions(nameof(Fail_WhenRatingIsNotInRange)+ testRating)))
            {

                arrangeContext.User.Add(new User()
                {
                    Name = "User1"
                });

                arrangeContext.Book.Add(new Book()
                {
                    BookId = 1,
                    Title = "TestTitle"
                });
                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new BookInspectorContext(TestUtils.GetOptions(nameof(Fail_WhenRatingIsNotInRange)+testRating)))
            {
                var sut = new RatingService(actAndAssertContext);

                var exception = Assert.ThrowsException<ArgumentException>(
                    () => sut.AddRating("TestTitle", "User1", testRating),
                    "Expected Arguement exception not thrown");
                Assert.AreEqual($"Invalid rating. Rating should be between 1 and 5.", exception.Message, "Arguement exception message is not correct");
            }
        }


        [TestMethod]
        public void Suceed_WhenUserDoesNotHavePreviosRatingForTheBook()
        {
         

            using (var arrangeContext = new BookInspectorContext(TestUtils.GetOptions(nameof(Suceed_WhenUserDoesNotHavePreviosRatingForTheBook))))
            {
                arrangeContext.User.Add(new User()
                {
                    UserId = 1,
                    Name = "User1"
                });

                arrangeContext.Book.Add(new Book()
                {
                    BookId = 1,
                    Title = "TestTitle"
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new BookInspectorContext(TestUtils.GetOptions(nameof(Suceed_WhenUserDoesNotHavePreviosRatingForTheBook))))
            {
                var sut = new RatingService(actAndAssertContext);

                var rating = sut.AddRating("TestTitle", "User1", 4);

                Assert.AreEqual(4, rating.Rating);
                Assert.AreEqual("TestTitle", rating.Book.Title);
                Assert.AreEqual("User1", rating.User.Name);
                Assert.AreEqual(1, rating.BookId);
                Assert.AreEqual(1, rating.UserId);
            }
        }

        [TestMethod]
        public void Fail_WhenUserHasPreviosRatingForTheBook()
        {

            using (var arrangeContext = new BookInspectorContext(TestUtils.GetOptions(nameof(Fail_WhenUserHasPreviosRatingForTheBook))))
            {
                arrangeContext.User.Add(new User()
                {
                    UserId = 1,
                    Name = "User1"
                });

                arrangeContext.Book.Add(new Book()
                {
                    BookId = 1,
                    Title = "TestTitle"
                });

                arrangeContext.RatingByBook.Add(new RatingForBookByUser()
                {
                    BookId = 1, 
                    UserId = 1, 
                    Rating = 4
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new BookInspectorContext(TestUtils.GetOptions(nameof(Fail_WhenUserHasPreviosRatingForTheBook))))
            {
                var sut = new RatingService(actAndAssertContext);

                var exception = Assert.ThrowsException<ArgumentException>(
                     () => sut.AddRating("TestTitle", "User1", 4),
                     "Expected Arguement exception not thrown");
                Assert.AreEqual($"User User1 already has rated the book TestTitle", exception.Message, "Arguement exception message is not correct");

            }
        }


    }
}
