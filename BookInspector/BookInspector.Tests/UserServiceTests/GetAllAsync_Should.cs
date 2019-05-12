namespace BookInspector.Services.Tests.UserServiceTests
{
    using BookInspector.DATA;
    using BookInspector.DATA.Models;
    using BookInspector.SERVICES;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    public class GetAllAsync_Should
    {
        [TestMethod]
        public async Task LoadUsersAsync()
        {
            using (var arrangeContext = 
                new ApplicationDbContext(TestUtils.GetOptions(nameof(LoadUsersAsync))))
            {
                arrangeContext.Users.Add(new ApplicationUser()
                {
                    Id = "xxxx",
                    UserName = "User1",
                    Email = "email"
                });

                arrangeContext.Users.Add(new ApplicationUser()
                {
                    Id = "yyyy",
                    UserName = "User2",
                    Email = "email"
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(LoadUsersAsync))))
            {
                var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
                var mockUserRoleStore = mockUserStore.As<IUserRoleStore<ApplicationUser>>();

                var userManager = new UserManager<ApplicationUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);

                var sut = new UserService(actAndAssertContext, userManager);

                var users = await sut.GetAllAsync();

                Assert.IsNotNull(users);
                var usersList = users.ToList();
                Assert.AreEqual(2, usersList.Count);
                Assert.AreEqual("xxxx", usersList[0].Id);
                Assert.AreEqual("User1", usersList[0].Name);
                Assert.AreEqual("email", usersList[0].Email);
                Assert.IsNull(usersList[0].Roles);
                Assert.AreEqual("yyyy", usersList[1].Id);
                Assert.AreEqual("User2", usersList[1].Name);
                Assert.AreEqual("email", usersList[1].Email);
                Assert.IsNull(usersList[1].Roles);

            }
        }
    }
}

