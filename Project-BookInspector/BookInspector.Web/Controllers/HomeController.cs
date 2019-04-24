
namespace BookInspector.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Diagnostics;
    using BookInspector.Web.Models;
    using BookInspector.Data.Models;
    using BookInspector.Services.Contracts;
    using BookInspector.Web.Mappers.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IViewModelMapper<Book, BookViewModel> _searchMapper;

        public HomeController(IBookService bookService, IViewModelMapper<Book, BookViewModel> searchMapper)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _searchMapper = searchMapper ?? throw new ArgumentNullException(nameof(searchMapper));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search([FromQuery]SearchModel model)
        {
            if (string.IsNullOrWhiteSpace(model.SearchBook) ||
                model.SearchBook.Length < 1)
            {
                return View();
            }

            model.SearchResults = 
                _bookService.Search(model.SearchBook)
                            .Select(_searchMapper.MapFrom)
                            .ToList();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
