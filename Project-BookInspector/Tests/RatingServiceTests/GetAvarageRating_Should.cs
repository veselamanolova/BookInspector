namespace Tests.RatingServiceTests
{
    using System;
    using BookInspector.Services;
    using BookInspector.Data.Models;
    using BookInspector.Data.Context;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetAvarageRating_Should
    {
        //    [TestMethod]
        //    public void Succed_WhenBookExist()
        //    {
        //        using (var arrangeContext = new BookInspectorContext(TestUtils.GetOptions("GetAvarageRating" + nameof(Succed_WhenBookDoesNotExist))))
        //        {
        //            arrangeContext.User.Add(new User()
        //            {
        //                Name = "User1"
        //            });

        //            arrangeContext.User.Add(new User()
        //            {
        //                UserId = 2,
        //                Name = "User2"
        //            });

        //            arrangeContext.Book.Add(new Book()
        //            {
        //                BookId = 1,
        //                Title = "TestTitle"
        //            });

        //            arrangeContext.SaveChanges();
        //        }
        //    }

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
                var rating = sut.GetAvarageRating("TestTitle"); 
                Assert.AreEqual(0, rating );
            }
        }
    }
}


