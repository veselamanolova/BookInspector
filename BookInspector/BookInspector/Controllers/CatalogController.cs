
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
            // IEnumerable<CatalogListingModel> books = _bookService.GetAll() 
            IEnumerable<CatalogListingModel> books = _bookService.GetShortBooks()
              .Select(book => new CatalogListingModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    PublishedDate = book.PublishedDate,
                    Publisher = book.PublisherName,
                    // Category = GetCategories(book),                  
                    AuthorNames = book.AuthorNames,
                    ImageURL = book.ImageURL, 
                    ShortDescription = book.ShortDescription
                });

            var model = new CatalogIndexModel { BooksList = books };

            return View(model);
        }


      
        public IActionResult Details(int id)
        {
            //var book = _bookService.GetById(id);

            var book = _bookService.GetBookDetailsById(id); 

            var model = new DetailsIndexModel
            {
                Id = book.Id,
                Title = book.Title,
                Publisher = book.PublisherName,
                PublishedDate = book.PublishedDate,
                Categories = book.Categories,
                // Category = GetCategories(book),
                ImageURL = book.ImageURL,
                Authors = book.AuthorNames,                 
                // Authors = GetAuthorsFromBook(book)
                Description = book.Description, 
                PreviewLink = book.PreviewLink
            };

            return View(model);
        }

        private IEnumerable<string> GetCategories(Book book)
        {
            var list = new List<string>();

            foreach (var category in book.BooksCategories)
                list.Add(category.Category.CategoryName);

            return list;
        }


        //private IEnumerable<string> GetAuthorsFromBook(ShortBook book)
        //{
        //    var list = new List<string>();

        //    foreach (var author in book)
        //    {
        //            list.Add(author.AuthorName);
        //    }
                

        //    return list;
        //}
    }
}



