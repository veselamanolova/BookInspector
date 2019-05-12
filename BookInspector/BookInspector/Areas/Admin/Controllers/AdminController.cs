
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
    using BookInspector.SERVICES.DTOs;
    using BookInspector.Areas.Admin.Models;
    using Microsoft.AspNetCore.Identity;

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
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(IJsonBooksImporterService jsonBooksImporterService, IUserService userService, RoleManager<IdentityRole> roleManager)
        {
            _jsonBooksImporterService = jsonBooksImporterService ?? throw new ArgumentNullException(nameof(jsonBooksImporterService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
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
        [HttpGet("Users")]
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllAsync();        
            return View(users);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("EditUser")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.GetUser(id);
            var allRoles = _roleManager.Roles;



            UserViewModel userViewModel = new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Roles = allRoles.Select(role => new RoleViewModel
                {
                    Name = role.Name,
                    Selected = user.Roles.Contains(role.Name)
                }).ToList() 
            }; 

            return View(userViewModel);
        }


        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _userService.EditUser(model.Id,model.Name, model.Email, 
                    model.Roles
                        .Where(r => r.Selected)
                        .Select(r => r.Name)
                        .ToList());

                return RedirectToAction(nameof(Users));
            }
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError("Error", ex.Message);
                return View(model);
            }
        }       

    }
}