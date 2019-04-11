namespace Tests.RatingServiceTests
{
    using System;
    using BookInspector.Services;
    using BookInspector.Data.Models;
    using BookInspector.Data.Context;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetAverageRating_Should
    {      

        [TestMethod]
        public void Fail_WhenBookDoesNotExist()
        {

            using (var actAndAssertContext = new BookInspectorContext(TestUtils.GetOptions("GetAvarageRating" + nameof(Fail_WhenBookDoesNotExist))))
            {
                var sut = new RatingService(actAndAssertContext);

                var exception = Assert.ThrowsException<ArgumentException>(
                    () => sut.AddRating("TestTitle", "User1", 4),
                    "Expected Arguement exception not thrown");
                Assert.AreEqual($"The book does not exist!", exception.Message, "Arguement exception message is not correct");
            }
        }

        [TestMethod]
        public void Return0_WhenThereIsNoRatingForTheBook()
        {
            using (var arrangeContext = 
                new BookInspectorContext(TestUtils.GetOptions("GetAvarageRating" +
                nameof(Return0_WhenThereIsNoRatingForTheBook))))
            {

                arrangeContext.Book.Add(new Book()
                {
                    BookId = 1,
                    Title = "TestTitle"
                });

                arrangeContext.SaveChanges();
            }
            using (var actAndAssertContext = 
                new BookInspectorContext(TestUtils.GetOptions("GetAvarageRating" + 
                nameof(Return0_WhenThereIsNoRatingForTheBook))))
            {
                var sut = new RatingService(actAndAssertContext);
                var rating = sut.GetAverageRating("TestTitle"); 
                Assert.AreEqual(0, rating );
            }
        }

        [TestMethod]
        public void ReturnCorrectValue_WhenThereTwoRatingsForTheBook()
        {
            using (var arrangeContext =
                new BookInspectorContext(TestUtils.GetOptions("GetAvarageRating" +
                nameof(ReturnCorrectValue_WhenThereTwoRatingsForTheBook))))
            {

                arrangeContext.Book.Add(new Book()
                {
                    BookId = 1,
                    Title = "TestTitle"
                });

                arrangeContext.User.Add(new User()
                {
                    Name = "User1"
                });

                arrangeContext.User.Add(new User()
                {
                    UserId = 2,
                    Name = "User2"
                });

                arrangeContext.RatingByBook.Add(new RatingForBookByUser()
                {   
                    BookId = 1,
                    UserId =1,
                    Rating = 3

                });

                arrangeContext.RatingByBook.Add(new RatingForBookByUser()
                {
                    BookId = 1,
                    UserId = 2,
                    Rating = 5
                });


                arrangeContext.SaveChanges();
            }
            using (var actAndAssertContext =
                new BookInspectorContext(TestUtils.GetOptions("GetAvarageRating" +
                nameof(ReturnCorrectValue_WhenThereTwoRatingsForTheBook))))
            {
                var sut = new RatingService(actAndAssertContext);
                var rating = sut.GetAverageRating("TestTitle");
                Assert.AreEqual(4, rating);
            }
        }


        [TestMethod]
        public void ReturnCorrectValue_WhenThereThreeRatingsForTheBook()
        {
            using (var arrangeContext =
                new BookInspectorContext(TestUtils.GetOptions("GetAvarageRating" +
                nameof(ReturnCorrectValue_WhenThereThreeRatingsForTheBook))))
            {

                arrangeContext.Book.Add(new Book()
                {
                    BookId = 1,
                    Title = "TestTitle"
                });

                arrangeContext.User.Add(new User()
                {
                    UserId = 1,
                    Name = "User1"
                });

                arrangeContext.User.Add(new User()
                {
                    UserId = 2,
                    Name = "User2"
                });

                arrangeContext.User.Add(new User()
                {
                    UserId = 3,
                    Name = "User3"
                });

                arrangeContext.RatingByBook.Add(new RatingForBookByUser()
                {
                    BookId = 1,
                    UserId = 1,
                    Rating = 3

                });

                arrangeContext.RatingByBook.Add(new RatingForBookByUser()
                {
                    BookId = 1,
                    UserId = 2,
                    Rating = 5
                });

                arrangeContext.RatingByBook.Add(new RatingForBookByUser()
                {
                    BookId = 1,
                    UserId = 3,
                    Rating = 4
                });

                arrangeContext.SaveChanges();
            }
            using (var actAndAssertContext =
                new BookInspectorContext(TestUtils.GetOptions("GetAvarageRating" +
                nameof(ReturnCorrectValue_WhenThereThreeRatingsForTheBook))))
            {
                var sut = new RatingService(actAndAssertContext);
                var rating = sut.GetAverageRating("TestTitle");
                Assert.AreEqual(4, rating);
            }
        }
    }
}


