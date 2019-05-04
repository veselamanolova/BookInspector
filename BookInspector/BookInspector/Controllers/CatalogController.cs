
namespace BookInspector.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using BookInspector.Models.Catalog;
    using BookInspector.SERVICES.Contracts;
    using BookInspector.DATA.Models;

    public class CatalogController : Controller
    {
        private readonly IBookService _bookService;

        public CatalogController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            IEnumerable<CatalogListingModel> books = _bookService.GetAll()
                .Select(book => new CatalogListingModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    PublishedDate = book.PublishedDate,
                    Publisher = book.Publisher.PublisherName,
                    // Category = GetCategories(book),
                    ImageURL = book.ImageURL
                });

            var model = new CatalogIndexModel { BooksList = books };

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var book = _bookService.GetById(id);

            var model = new DetailsIndexModel
            {
                Id = book.Id,
                Title = book.Title,
                Publisher = book.Publisher.PublisherName,
                PublishedDate = book.PublishedDate,
                // Category = GetCategories(book),
                ImageURL = book.ImageURL,
                // Authors = GetAuthorsFromBook(book)
            };

            return View(model);
        }

        private IEnumerable<string> GetCategories(Book book)
        {
            var list = new List<string>();

            foreach (var category in book.BookCategory)
                list.Add(category.Category.CategoryName);

            return list;
        }
    }
}



