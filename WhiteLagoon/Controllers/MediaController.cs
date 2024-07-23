using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using LibraryBook.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace LibraryBook.Web.Controllers
{
    public class MediaController : Controller
    {
        private readonly IMediaService _mediaService;
        private readonly IMediaCategoryService _mediaCategoryService;
        public MediaController(IMediaService mediaService, IMediaCategoryService mediaCategoryService)
        {
            _mediaService = mediaService;
            _mediaCategoryService = mediaCategoryService;
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
        public IActionResult Create(MediaVM obj)
        {
            if (ModelState.IsValid)
            {
                obj.Media.IsActive = true;
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
        public IActionResult Update(MediaVM mediaVM)
        {
            mediaVM.MediaCategoryList = _mediaCategoryService.GetAllMediaCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            if (ModelState.IsValid && mediaVM.Media.Id > 0)
            {
                mediaVM.Media.IsActive = true;
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