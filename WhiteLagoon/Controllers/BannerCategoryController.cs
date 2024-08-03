using LibraryBook.Application.Common.Utility;
using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Manager)]
    public class BannerCategoryController : Controller
    {
        private readonly IBannerCategoryService _BannerCategoryService;
        public BannerCategoryController(IBannerCategoryService BannerCategoryService)
        {
            _BannerCategoryService = BannerCategoryService;                
        }
        public IActionResult Index()
        {
            var BannerCategories = _BannerCategoryService.GetAllBannerCategory();
            return View(BannerCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BannerCategory obj)
        {           
            if (ModelState.IsValid)
            {

                _BannerCategoryService.CreateBannerCategory(obj);
                TempData["success"] = "Create successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Create failed";
            return View();
        }
        public IActionResult Update(int bannerCategoryId)
        {
            BannerCategory? obj = _BannerCategoryService.GetBannerCategoryById(bannerCategoryId);

            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(BannerCategory obj)
        {

            if (ModelState.IsValid && obj.Id > 0)
            {

                _BannerCategoryService.UpdateBannerCategory(obj);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Updated failed";
            return View();
        }
        public IActionResult Delete(int bannerCategoryId)
        {
            BannerCategory? obj = _BannerCategoryService.GetBannerCategoryById(bannerCategoryId);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(BannerCategory obj)
        {
            bool deleted = _BannerCategoryService.DeleteBannerCategory(obj.Id);
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
