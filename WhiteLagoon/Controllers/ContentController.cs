using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using LibraryBook.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace LibraryBook.Web.Controllers
{
    public class ContentController : Controller
    {
        private readonly IContentService _contentService;
        private readonly IContentCategoryService _contentCategoryService;
        public ContentController(IContentService contentService, IContentCategoryService contentCategoryService)
        {
            _contentService = contentService;
            _contentCategoryService = contentCategoryService;                       
        }
        public IActionResult Index()
        {
            var contents = _contentService.GetAllContent();
            return View(contents);
        }
        public IActionResult Create()
        {
            ContentVM contentVM = new()
            {
                ContentCategoryList = _contentCategoryService.GetAllContentCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
            };
            return View(contentVM);
        }
        [HttpPost]
        public IActionResult Create(ContentVM obj)
        {
            if (ModelState.IsValid)
            {                
                obj.Content.IsActive = true;
                _contentService.CreateContent(obj.Content);
                TempData["success"] = "Create successfully";
                return RedirectToAction(nameof(Index));
            }
            obj.ContentCategoryList = _contentCategoryService.GetAllContentCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });           
            TempData["error"] = "Create failed";
            return View(obj);
        }
        public IActionResult Update(int contentId)
        {

            ContentVM contentVM = new()
            {
                ContentCategoryList = _contentCategoryService.GetAllContentCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Content = _contentService.GetContentById(contentId)
            };
            if (contentVM == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(contentVM);
        }

        [HttpPost]
        public IActionResult Update(ContentVM contentVM)
        {
            contentVM.ContentCategoryList = _contentCategoryService.GetAllContentCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            if (ModelState.IsValid && contentVM.Content.Id > 0)
            {                        
                contentVM.Content.IsActive = true;
                _contentService.UpdateContent(contentVM.Content);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Updated failed";
            return View(contentVM);
        }
        public IActionResult Delete(int contentId)
        {
            ContentVM contentVM = new()
            {
                ContentCategoryList = _contentCategoryService.GetAllContentCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Content = _contentService.GetContentById(contentId)
            };
            if (contentVM == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(contentVM);
        }

        [HttpPost]
        public IActionResult Delete(ContentVM contentVM)
        {
            Content? objFromDb = _contentService.GetContentById(contentVM.Content.Id);
            if (objFromDb is not null)
            {
                _contentService.DeleteContent(objFromDb.Id);

                TempData["success"] = "Deleted successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Deleted failed";
            return View();
        }

        public IActionResult ChangeStatusContent(int contentId)
        {
            Content? content = _contentService.GetContentById(contentId);
            if (content is not null)
            {
                content.IsActive = !content.IsActive; //Toggle the status
                _contentService.UpdateContent(content);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}