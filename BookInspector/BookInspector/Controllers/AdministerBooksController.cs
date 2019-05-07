namespace BookInspector.Controllers
{
    using System;
    using System.Linq;
    using BookInspector.Models.AdministerBooksViewModels;
    using BookInspector.SERVICES.Contracts;
    using BookInspector.SERVICES.Json;
    using Microsoft.AspNetCore.Mvc;

    public class AdministerBooksController : Controller
    {
        private readonly IJsonBooksImporterService _jsonBooksImporterService;

        public AdministerBooksController(IJsonBooksImporterService jsonBooksImporterService)
        {
            _jsonBooksImporterService = jsonBooksImporterService ?? throw new ArgumentNullException(nameof(_jsonBooksImporterService)); ;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ImportBookFromJsonViewModel vm = new ImportBookFromJsonViewModel();
            return View(vm);

        }

        [HttpPost]
        public IActionResult Index(string url)
        {
            
            try
            {
                var books = _jsonBooksImporterService.ImportBooks(url, true);
                //return View();

                return RedirectToAction("Index","Catalog");

            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View();
            }

        }
    }
}