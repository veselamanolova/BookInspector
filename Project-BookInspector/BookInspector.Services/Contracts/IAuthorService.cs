
namespace BookInspector.Services.Contracts
{
    using BookInspector.Data.Models;
    using System.Collections.Generic;

    public interface IAuthorService
    {
        Author Add(string name);

        IReadOnlyCollection<Author> GetAuthors();
    }
}


