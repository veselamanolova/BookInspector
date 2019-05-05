
namespace BookInspector.SERVICES.Contracts
{
    using BookInspector.DATA.Models;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        Category AddCategory(string name);

        IReadOnlyCollection<Category> ShowAllCategories();
    }
}