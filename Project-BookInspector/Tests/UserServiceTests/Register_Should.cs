
using System;

namespace Tests.UserServiceTests
{
    using BookInspector.Services;
    using BookInspector.Data.Context;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BookInspector.Data.Models;

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
        [DataRow("u")]
        [DataRow("uuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu")]
        public void RegisterUserThrowIfIsNotInRange(string name)
        {
            var options = TestUtils.GetOptions(nameof(RegisterUserThrowIfIsNotInRange));

            using (var actAndAssertContext = new BookInspectorContext(options))
            {
                var sut = new UserService(actAndAssertContext);

                var ex = Assert.ThrowsException<ArgumentException>(() => sut.Register(name));

                string exEx = "Value does not fall within the expected range.";

                Assert.AreEqual(exEx, ex.Message);
            }
        }
    }
}

