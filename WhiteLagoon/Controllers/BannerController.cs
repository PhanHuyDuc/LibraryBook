using LibraryBook.Application.Common.Utility;
using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using LibraryBook.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace LibraryBook.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Manager)]
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IBannerCategoryService _bannerCategoryService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public BannerController(IBannerService bannerService, IBannerCategoryService bannerCategoryService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _bannerService = bannerService;
            _bannerCategoryService = bannerCategoryService;  
            _userManager = userManager;
            _roleManager = roleManager;
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
        public async Task<IActionResult> Create(BannerVM obj)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null && await _userManager.IsInRoleAsync(user, SD.Role_Admin))
                {
                    obj.Banner.IsActive = true;
                }
                else
                {
                    obj.Banner.IsActive = false;
                }
                    
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
        public async Task<IActionResult> Update(BannerVM bannerVM)
        {
            bannerVM.BannerCategoryList = _bannerCategoryService.GetAllBannerCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            if (ModelState.IsValid && bannerVM.Banner.Id > 0)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null && await _userManager.IsInRoleAsync(user, SD.Role_Admin))
                {
                    bannerVM.Banner.IsActive = true;
                }
                else
                {
                    bannerVM.Banner.IsActive = false;
                }
                    
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