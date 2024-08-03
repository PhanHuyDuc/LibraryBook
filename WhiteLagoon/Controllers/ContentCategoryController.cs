using LibraryBook.Application.Common.Utility;
using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Manager)]
    public class ContentCategoryController : Controller
    {
        private readonly IContentCategoryService _ContentCategoryService;
        public ContentCategoryController(IContentCategoryService ContentCategoryService)
        {
            _ContentCategoryService = ContentCategoryService;                
        }
        public IActionResult Index()
        {
            var ContentCategories = _ContentCategoryService.GetAllContentCategory();
            return View(ContentCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ContentCategory obj)
        {           
            if (ModelState.IsValid)
            {

                _ContentCategoryService.CreateContentCategory(obj);
                TempData["success"] = "Create successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Create failed";
            return View();
        }
        public IActionResult Update(int ContentCategoryId)
        {
            ContentCategory? obj = _ContentCategoryService.GetContentCategoryById(ContentCategoryId);

            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(ContentCategory obj)
        {

            if (ModelState.IsValid && obj.Id > 0)
            {

                _ContentCategoryService.UpdateContentCategory(obj);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Updated failed";
            return View();
        }
        public IActionResult Delete(int ContentCategoryId)
        {
            ContentCategory? obj = _ContentCategoryService.GetContentCategoryById(ContentCategoryId);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(ContentCategory obj)
        {
            bool deleted = _ContentCategoryService.DeleteContentCategory(obj.Id);
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
