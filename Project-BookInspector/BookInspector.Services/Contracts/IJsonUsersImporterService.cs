namespace BookInspector.Services.Contracts
{
    using System.Collections.Generic;
    using BookInspector.Data.Models;

    public interface IJsonUsersImporterService
    {
        List<User> ImportUsers(string filePath);
    }
}