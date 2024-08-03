using LibraryBook.Application.Common.Utility;
using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Manager)]
    public class WidgetContentCategoryController : Controller
    {
        private readonly IWidgetContentCategoryService _widgetContentCategoryService;
        public WidgetContentCategoryController(IWidgetContentCategoryService widgetContentCategoryService)
        {
            _widgetContentCategoryService = widgetContentCategoryService;
        }
        public IActionResult Index()
        {
            var widgetContentCategories = _widgetContentCategoryService.GetAllWidgetContentCategory();
            return View(widgetContentCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(WidgetContentCategory obj)
        {           
            if (ModelState.IsValid)
            {

                _widgetContentCategoryService.CreateWidgetContentCategory(obj);
                TempData["success"] = "Create successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Create failed";
            return View();
        }
        public IActionResult Update(int widgetContentCategoryId)
        {
            WidgetContentCategory? obj = _widgetContentCategoryService.GetWidgetContentCategoryById(widgetContentCategoryId);

            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(WidgetContentCategory obj)
        {

            if (ModelState.IsValid && obj.Id > 0)
            {

                _widgetContentCategoryService.UpdateWidgetContentCategory(obj);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Updated failed";
            return View();
        }
        public IActionResult Delete(int widgetContentCategoryId)
        {
            WidgetContentCategory? obj = _widgetContentCategoryService.GetWidgetContentCategoryById(widgetContentCategoryId);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(WidgetContentCategory obj)
        {
            bool deleted = _widgetContentCategoryService.DeleteWidgetContentCategory(obj.Id);
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
