
namespace BookInspector.Services.Contracts
{
    using BookInspector.Data.Models;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        Category AddCategory(string name);

        IReadOnlyCollection<Category> ShowAllCategories();
    }
}