namespace BookInspector.Services.Tests.UserServiceTests
{
    using BookInspector.DATA;
    using BookInspector.DATA.Models;
    using BookInspector.SERVICES;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    public class GetUser_Should
    {
        [TestMethod]
        public async Task ReturnNullIfUserDoesNotExistAsync()
        {
           
            using (var actAndAssertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(ReturnNullIfUserDoesNotExistAsync))))
            {
                var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
                var userManager = new UserManager<ApplicationUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);

                var sut = new UserService(actAndAssertContext, userManager);

                var user = await sut.GetUser("xxxx");

                Assert.IsNull(user);

            }
        }


        [TestMethod]
        public async Task LoadUserIfUserExistsAsync()
        {
            using (var arrangeContext = 
                new ApplicationDbContext(TestUtils.GetOptions(nameof(LoadUserIfUserExistsAsync))))
            {
                arrangeContext.Users.Add(new ApplicationUser()
                {
                    Id = "xxxx",
                    UserName = "User1", 
                    Email="email"
                });

                arrangeContext.SaveChanges();
            }

            using (var actAndAssertContext = new ApplicationDbContext(TestUtils.GetOptions(nameof(LoadUserIfUserExistsAsync))))
            {
                var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
                var mockUserRoleStore = mockUserStore.As<IUserRoleStore<ApplicationUser>>();
                mockUserRoleStore.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<string> { "User", "Administrator" });

                var userManager = new UserManager<ApplicationUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);

                var sut = new UserService(actAndAssertContext, userManager);

                var user = await sut.GetUser("xxxx");

                Assert.IsNotNull(user);
                Assert.AreEqual("xxxx", user.Id);
                Assert.AreEqual("User1", user.Name);
                Assert.AreEqual("email", user.Email);
                Assert.IsNotNull(user.Roles);
                Assert.AreEqual(2, user.Roles.Count);
                Assert.AreEqual("User", user.Roles[0]);
                Assert.AreEqual("Administrator", user.Roles[1]);

            }
        }
    }
}

