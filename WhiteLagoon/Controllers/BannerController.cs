using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using LibraryBook.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace LibraryBook.Web.Controllers
{
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IBannerCategoryService _bannerCategoryService;
        public BannerController(IBannerService bannerService, IBannerCategoryService bannerCategoryService)
        {
            _bannerService = bannerService;
            _bannerCategoryService = bannerCategoryService;            
        }
        public IActionResult Index()
        {
            var banners = _bannerService.GetAllBanner();
            return View(banners);
        }
        public IActionResult Create()
        {
            BannerVM bannerVM = new()
            {
                BannerCategoryList = _bannerCategoryService.GetAllBannerCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
            };
            return View(bannerVM);
        }
        [HttpPost]
        public IActionResult Create(BannerVM obj)
        {
            if (ModelState.IsValid)
            {                
                obj.Banner.IsActive = true;
                _bannerService.CreateBanner(obj.Banner);
                TempData["success"] = "Create successfully";
                return RedirectToAction(nameof(Index));
            }
            obj.BannerCategoryList = _bannerCategoryService.GetAllBannerCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });           
            TempData["error"] = "Create failed";
            return View(obj);
        }
        public IActionResult Update(int bannerId)
        {

            BannerVM bannerVM = new()
            {
                BannerCategoryList = _bannerCategoryService.GetAllBannerCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Banner = _bannerService.GetBannerById(bannerId)
            };
            if (bannerVM == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(bannerVM);
        }

        [HttpPost]
        public IActionResult Update(BannerVM bannerVM)
        {
            bannerVM.BannerCategoryList = _bannerCategoryService.GetAllBannerCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            if (ModelState.IsValid && bannerVM.Banner.Id > 0)
            {                        
                bannerVM.Banner.IsActive = true;
                _bannerService.UpdateBanner(bannerVM.Banner);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Updated failed";
            return View(bannerVM);
        }
        public IActionResult Delete(int bannerId)
        {
            BannerVM bannerVM = new()
            {
                BannerCategoryList = _bannerCategoryService.GetAllBannerCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Banner = _bannerService.GetBannerById(bannerId)
            };
            if (bannerVM == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(bannerVM);
        }

        [HttpPost]
        public IActionResult Delete(BannerVM bannerVM)
        {
            Banner? objFromDb = _bannerService.GetBannerById(bannerVM.Banner.Id);
            if (objFromDb is not null)
            {
                _bannerService.DeleteBanner(objFromDb.Id);

                TempData["success"] = "Deleted successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Deleted failed";
            return View();
        }

        public IActionResult ChangeStatusBanner(int bannerId)
        {
            Banner? banner = _bannerService.GetBannerById(bannerId);
            if (banner is not null)
            {
                banner.IsActive = !banner.IsActive; //Toggle the status
                _bannerService.UpdateBanner(banner);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}