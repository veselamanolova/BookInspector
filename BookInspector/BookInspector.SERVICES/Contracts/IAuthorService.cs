
namespace BookInspector.SERVICES.Contracts
{
    using BookInspector.DATA.Models;
    using System.Collections.Generic;

    public interface IAuthorService
    {
        Author Add(string name);

        Dictionary<string, List<string>> Search(string name);

        IReadOnlyCollection<Author> GetAuthors();
    }
}


