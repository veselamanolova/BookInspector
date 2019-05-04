
namespace BookInspector.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using BookInspector.Models.Catalog;
    using BookInspector.SERVICES.Contracts;
    using BookInspector.DATA.Models;
    using System;

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
                     ShortDescription = book.ShortDescription,
                     Publisher = book.PublisherName,                    
                     AuthorNames = book.AuthorNames, 
                     ImageURL = book.ImageURL
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
                PublishedDate = book.PublishedDate,                
                Publisher = book.PublisherName,
                Description = book.Description,
                Authors = book.AuthorNames,
                Categories = book.Categories,
                PreviewLink = book.PreviewLink,
                ImageURL = book.ImageURL
            };

            return View(model);
        }

        private IEnumerable<string> GetCategoriesFromBook(Book book)
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



