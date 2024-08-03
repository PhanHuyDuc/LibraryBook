using LibraryBook.Application.Common.Utility;
using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Manager)]
    public class MenuCategoryController : Controller
    {
        private readonly IMenuCategoryService _menuCategoryService;
        public MenuCategoryController(IMenuCategoryService menuCategoryService)
        {
            _menuCategoryService = menuCategoryService;                
        }
        public IActionResult Index()
        {
            var menuCategories = _menuCategoryService.GetAllMenuCategory();
            return View(menuCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(MenuCategory obj)
        {           
            if (ModelState.IsValid)
            {

                _menuCategoryService.CreateMenuCategory(obj);
                TempData["success"] = "Create successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Create failed";
            return View();
        }
        public IActionResult Update(int menuCategoryId)
        {
            MenuCategory? obj = _menuCategoryService.GetMenuCategoryById(menuCategoryId);

            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(MenuCategory obj)
        {

            if (ModelState.IsValid && obj.Id > 0)
            {

                _menuCategoryService.UpdateMenuCategory(obj);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Updated failed";
            return View();
        }
        public IActionResult Delete(int menuCategoryId)
        {
            MenuCategory? obj = _menuCategoryService.GetMenuCategoryById(menuCategoryId);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(MenuCategory obj)
        {
            bool deleted = _menuCategoryService.DeleteMenuCategory(obj.Id);
            if (deleted)
            {
                TempData["success"] = "Deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Deleted failed";
            }
            return View();
        }
    }
}
