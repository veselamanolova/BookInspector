
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
    using System.Collections.Generic;

    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IBookViewModelMapper<Book, BookViewModel> _bookViewMapper;

        public HomeController(IBookService bookService, IBookViewModelMapper<Book, BookViewModel> bookViewMapper)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _bookViewMapper = bookViewMapper ?? throw new ArgumentNullException(nameof(bookViewMapper));
        }

        public IActionResult Index([FromQuery]BookViewModel model)
        {
            model._results = _bookService.GetAll()
                .Select(_bookViewMapper.MapFrom)
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
