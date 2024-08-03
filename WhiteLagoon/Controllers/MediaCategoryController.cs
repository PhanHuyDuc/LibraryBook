using LibraryBook.Application.Common.Utility;
using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Manager)]
    public class MediaCategoryController : Controller
    {
        private readonly IMediaCategoryService _mediaCategoryService;
        public MediaCategoryController(IMediaCategoryService mediaCategoryService)
        {
            _mediaCategoryService = mediaCategoryService;               
        }
        public IActionResult Index()
        {
            var mediaCategories = _mediaCategoryService.GetAllMediaCategory();
            return View(mediaCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(MediaCategory obj)
        {           
            if (ModelState.IsValid)
            {

                _mediaCategoryService.CreateMediaCategory(obj);
                TempData["success"] = "Create successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Create failed";
            return View();
        }
        public IActionResult Update(int mediaCategoryId)
        {
            MediaCategory? obj = _mediaCategoryService.GetMediaCategoryById(mediaCategoryId);

            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(MediaCategory obj)
        {

            if (ModelState.IsValid && obj.Id > 0)
            {

                _mediaCategoryService.UpdateMediaCategory(obj);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Updated failed";
            return View();
        }
        public IActionResult Delete(int mediaCategoryId)
        {
            MediaCategory? obj = _mediaCategoryService.GetMediaCategoryById(mediaCategoryId);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(MediaCategory obj)
        {
            bool deleted = _mediaCategoryService.DeleteMediaCategory(obj.Id);
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
