
namespace BookInspector.Services.Contracts
{
    using BookInspector.Data.Models;
    using System.Collections.Generic;

    public interface IUserService
    {
        User Register(string name);

        User FindByName(string name);

        User DeteleUser(string name);

        IReadOnlyCollection<User> GetUsers(int skip, int take);
    }
}