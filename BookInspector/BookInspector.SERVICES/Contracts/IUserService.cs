
namespace BookInspector.SERVICES.Contracts
{
    using BookInspector.DATA.Models;
    using System.Collections.Generic;

    public interface IUserService
    {
        ApplicationUser Register(string name);

        ApplicationUser FindByName(string name);

        ApplicationUser DeteleUser(string name);

        ApplicationUser Modify(string oldVal, string newVal);

        IReadOnlyCollection<ApplicationUser> GetUsers(int skip, int take);
    }
}