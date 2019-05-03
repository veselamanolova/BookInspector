
namespace BookInspector.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using BookInspector.Models.Catalog;
    using BookInspector.SERVICES.Contracts;

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
                    Category = book.Category.CategoryName,
                    ImageURL = book.ImageURL
                });

            var model = new CatalogIndexModel { BooksList = books };

            return View(model);
        }
    }
}


