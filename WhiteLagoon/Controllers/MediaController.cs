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
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly IMediaCategoryService _mediaCategoryService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public MediaController(
            IMediaService mediaService, 
            IMediaCategoryService mediaCategoryService, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _mediaService = mediaService;
            _mediaCategoryService = mediaCategoryService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var medias = _mediaService.GetAllMedia();
            return View(medias);
        }
        public IActionResult Create()
        {
            MediaVM mediaVM = new()
            {
                MediaCategoryList = _mediaCategoryService.GetAllMediaCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
            };
            return View(mediaVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MediaVM obj)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user!= null && await _userManager.IsInRoleAsync(user,SD.Role_Admin))
                {
                    obj.Media.IsActive = true;
                }
                else
                {
                    obj.Media.IsActive = false;
                }
                
                _mediaService.CreateMedia(obj.Media);
                TempData["success"] = "Create successfully";
                return RedirectToAction(nameof(Index));
            }
            obj.MediaCategoryList = _mediaCategoryService.GetAllMediaCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            TempData["error"] = "Create failed";
            return View(obj);
        }
        public IActionResult Update(int mediaId)
        {

            MediaVM mediaVM = new()
            {
                MediaCategoryList = _mediaCategoryService.GetAllMediaCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Media = _mediaService.GetMediaById(mediaId)
            };
            if (mediaVM == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(mediaVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MediaVM mediaVM)
        {
            mediaVM.MediaCategoryList = _mediaCategoryService.GetAllMediaCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            if (ModelState.IsValid && mediaVM.Media.Id > 0)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null && await _userManager.IsInRoleAsync(user, SD.Role_Admin))
                {
                    mediaVM.Media.IsActive = true;
                }
                else
                {
                    mediaVM.Media.IsActive = false;
                }
                
                _mediaService.UpdateMedia(mediaVM.Media);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Updated failed";
            return View(mediaVM);
        }
        public IActionResult Delete(int mediaId)
        {
            MediaVM mediaVM = new()
            {
                MediaCategoryList = _mediaCategoryService.GetAllMediaCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Media = _mediaService.GetMediaById(mediaId)
            };
            if (mediaVM == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(mediaVM);
        }

        [HttpPost]
        public IActionResult Delete(MediaVM mediaVM)
        {
            Media? objFromDb = _mediaService.GetMediaById(mediaVM.Media.Id);
            if (objFromDb is not null)
            {
                _mediaService.DeleteMedia(objFromDb.Id);

                TempData["success"] = "Deleted successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Deleted failed";
            return View();
        }

        public IActionResult ChangeStatusMedia(int mediaId)
        {
            Media? media = _mediaService.GetMediaById(mediaId);
            if (media is not null)
            {
                media.IsActive = !media.IsActive; //Toggle the status
                _mediaService.UpdateMedia(media);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}