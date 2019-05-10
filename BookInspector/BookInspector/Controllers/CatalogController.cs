
namespace BookInspector.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BookInspector.Models.Catalog;
    using BookInspector.SERVICES.Contracts;
    using System.Threading.Tasks;

    public class CatalogController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IExportService _export;

        public CatalogController(IBookService bookService, IExportService export)
        {
            _bookService = bookService;
            _export = export;
        }


        public IActionResult Index()
        {
            IEnumerable<CatalogListingModel> books = _bookService.GetAll()
                .Select(book => new CatalogListingModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    ImageURL = book.ImageURL,
                    Categories = book.BooksCategories.Select(x => x.Category).ToList(),
                    ShortDescription = book.ShortDescription
                }).ToList();

            var model = new CatalogIndexModel { BooksList = books };

            return View(model);
        }


        public IActionResult Category(string category)
        {
            IEnumerable<CatalogListingModel> books = _bookService.GetByCategory(category)
                .Select(book => new CatalogListingModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    ImageURL = book.ImageURL,
                    Categories = book.BooksCategories.Select(x => x.Category).ToList(),
                    ShortDescription = book.ShortDescription
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
                PublishedDate = book.PublishedDate,
                ImageURL = book.ImageURL,
                Description = book.Description, 
                PreviewLink = book.PreviewLink
            };

            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddBook()
        {
            return View();
        }


        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            await _bookService.DeleteBookAsync(Id);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Download()
        {
            _export.ExportToPDF();

            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult Next()
        {
            return View();
        }

        public IActionResult Previous()
        {
            return View();
        }
    }
}



