
namespace BookInspector.Services
{
    using System;
    using System.Linq;
    using BookInspector.Data.Models;
    using System.Collections.Generic;
    using BookInspector.Data.Repository;
    using BookInspector.Services.Contracts;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public Category AddCategory(string name)
        {
            var category = _categoryRepository.All().Where(x => x.Name.Equals(name)).SingleOrDefault();
            Validator.IfNotNull<ArgumentException>(category, $"Category {name} already exists");

            category = new Category() { Name = name };
            _categoryRepository.Add(category);
            _categoryRepository.Save();
            return category;
        }

        public IReadOnlyCollection<Category> ShowAllCategories()
        {
            return _categoryRepository.All().ToList();
        }
    }
}

