
namespace BookInspector.Area.Controllers
{
    using System;
    using System.Linq;
    using BookInspector.Models.AdministerBooksViewModels;
    using BookInspector.SERVICES.Contracts;
    using BookInspector.SERVICES.Json;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;


    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        public IActionResult Delete()
        {
            return View();
        }

        private readonly IJsonBooksImporterService _jsonBooksImporterService;

        public AdminController(IJsonBooksImporterService jsonBooksImporterService)
        {
            _jsonBooksImporterService = jsonBooksImporterService ?? throw new ArgumentNullException(nameof(_jsonBooksImporterService)); ;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Index()
        {
            ImportBookFromJsonViewModel vm = new ImportBookFromJsonViewModel();
            return View(vm);

        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult Index(string url)
        {

            try
            {
                var books = _jsonBooksImporterService.ImportBooks(url, true);
                //return View();

                return RedirectToAction("Index", "Catalog");

            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View();
            }

        }
    }
}