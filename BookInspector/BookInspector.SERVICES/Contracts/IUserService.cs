
namespace BookInspector.SERVICES.Contracts
{
    using BookInspector.DATA.Models;
    using System.Collections.Generic;

    public interface IUserService
    {
        DbUser Register(string name);

        DbUser FindByName(string name);

        DbUser DeteleUser(string name);

        DbUser Modify(string oldVal, string newVal);

        IReadOnlyCollection<DbUser> GetUsers(int skip, int take);
    }
}