
namespace Tests.UserServiceTests
{
    using BookInspector.Services;
    using BookInspector.Data.Models;
    using BookInspector.Data.Context;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Register_Should
    {
        [TestMethod]
        public void RegisterUserIsCreated()
        {
            var options = TestUtils.GetOptions(nameof(RegisterUserIsCreated));

            using (var actAndAssertContext = new BookInspectorContext(options))
            {
                var sut = new UserService(actAndAssertContext);

                var user = sut.Register("user");

                Assert.AreEqual("user", user.Name);
                Assert.IsNotNull(user);
            }
        }

        [TestMethod]
        public void FindByNameReturnCorrect()
        {
            var options = TestUtils.GetOptions(nameof(FindByNameReturnCorrect));

            using (var arrange = new BookInspectorContext(options))
            {
                var user = new User() { Name = "Tedi"};
                arrange.User.Add(user);
                arrange.SaveChanges();
            }

            using (var actAndAssertContext = new BookInspectorContext(options))
            {
                var sut = new UserService(actAndAssertContext);

                var fndUser = sut.FindByName("Tedi");

                Assert.IsTrue(fndUser.Name.Equals("Tedi"));
            }
        }
    }
}

