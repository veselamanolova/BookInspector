
namespace BookInspector.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using BookInspector.Models.Catalog;
    using BookInspector.SERVICES.Contracts;
    using System;

    public class CatalogController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IExportService _export;

        public CatalogController(IBookService bookService, IExportService export)
        {
            _bookService = bookService;
            _export = export;
        }


        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 60)]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["Filter"] = searchString;

            IEnumerable<CatalogListingModel> books = (await _bookService.GetAllAsync())
                .Select(book => new CatalogListingModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    ImageURL = book.ImageURL,
                    Categories = book.BooksCategories.Select(x => x.Category).ToList(),
                    ShortDescription = book.ShortDescription
                }).ToList();

            if (!String.IsNullOrEmpty(searchString))
                books = books
                    .Where(filter => filter.Title.ToLower().Contains(searchString.ToLower()));

            var model = new CatalogIndexModel { BooksList = books };

            return View(model);
        }


        public async Task<IActionResult> Category(string category)
        {
            IEnumerable<CatalogListingModel> books = (await  _bookService.GetByCategoryAsync(category))
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


        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

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
        [ValidateAntiForgeryToken]
        public IActionResult AddBook()
        {
            return View();
        }


        [HttpPost]
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
    }
}



