
namespace BookInspector.Area.Admin.Controllers
{
    using System;
    using System.Linq;
    using BookInspector.Models.AdministerBooksViewModels;
    using BookInspector.SERVICES.Contracts;
    using BookInspector.SERVICES.Json;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;    
    using System.Collections.Generic;
    using BookInspector.DATA.Models;
    using System.Threading.Tasks;

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        public IActionResult Delete()
        {
            return View();
        }

        private readonly IJsonBooksImporterService _jsonBooksImporterService;
        private readonly IUserService _userService;

        public AdminController(IJsonBooksImporterService jsonBooksImporterService, IUserService userService)
        {
            _jsonBooksImporterService = jsonBooksImporterService ?? throw new ArgumentNullException(nameof(jsonBooksImporterService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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

        [Authorize(Roles = "Administrator")]
        [HttpGet("Admin/Users")]
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllAsync();        
            return View(users);
        }
    }
}