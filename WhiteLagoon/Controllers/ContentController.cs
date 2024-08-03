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
    public class ContentController : Controller
    {
        private readonly IContentService _contentService;
        private readonly IContentCategoryService _contentCategoryService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ContentController(IContentService contentService, IContentCategoryService contentCategoryService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _contentService = contentService;
            _contentCategoryService = contentCategoryService;  
            _userManager = userManager;
            _roleManager = roleManager;
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
        public async Task<IActionResult> Create(ContentVM obj)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null && await _userManager.IsInRoleAsync(user, SD.Role_Admin))
                {
                    obj.Content.IsActive = true;
                }
                else
                {
                    obj.Content.IsActive = false;
                }
               
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
        
        public async Task<IActionResult> Update(ContentVM contentVM)
        {
            contentVM.ContentCategoryList = _contentCategoryService.GetAllContentCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            if (ModelState.IsValid && contentVM.Content.Id > 0)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null && await _userManager.IsInRoleAsync(user, SD.Role_Admin))
                {
                    contentVM.Content.IsActive = true;
                }
                else
                {
                    contentVM.Content.IsActive = false;
                }
               
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
        public IActionResult DeleteImage(int imageId, int contentId)
        {
            Content? content = _contentService.GetContentById(contentId);
            if (content is not null)
            {                
                _contentService.DeleteImage(imageId);
            }
            return RedirectToAction(nameof(Update), new {contentId = contentId });

        }

    }
}