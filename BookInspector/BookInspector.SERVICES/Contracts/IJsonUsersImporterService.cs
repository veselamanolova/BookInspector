namespace BookInspector.SERVICES.Contracts
{
    using System.Collections.Generic;
    using BookInspector.DATA.Models;

    public interface IJsonUsersImporterService
    {
        List<ApplicationUser> ImportUsers(string filePath);
    }
}