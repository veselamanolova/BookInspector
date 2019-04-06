
namespace BookInspector.Services.Contracts
{
    using BookInspector.Data.Models;
    using System.Collections.Generic;

    public interface IAuthorService
    {
        Author Add(string name);

        Author FindByName(string name);

        IReadOnlyCollection<Author> GetAuthors(int skip, int take);
    }
}


