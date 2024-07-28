using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using LibraryBook.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace LibraryBook.Web.Controllers
{
    public class WidgetContentController : Controller
    {
        private readonly IWidgetContentService _widgetContentService;
        private readonly IWidgetContentCategoryService _widgetContentCategoryService;
        public WidgetContentController(IWidgetContentService widgetContentService, IWidgetContentCategoryService widgetContentCategoryService)
        {
            _widgetContentService = widgetContentService;
            _widgetContentCategoryService = widgetContentCategoryService;            
        }
        public IActionResult Index()
        {
            var WidgetContents = _widgetContentService.GetAllWidgetContent();
            return View(WidgetContents);
        }
        public IActionResult Create()
        {
            WidgetContentVM widgetContentVM = new()
            {
                WidgetContentCategoryList = _widgetContentCategoryService.GetAllWidgetContentCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
            };
            return View(widgetContentVM);
        }
        [HttpPost]
        public IActionResult Create(WidgetContentVM obj)
        {
            if (ModelState.IsValid)
            {                
                obj.WidgetContent.IsActive = true;
                _widgetContentService.CreateWidgetContent(obj.WidgetContent);
                TempData["success"] = "Create successfully";
                return RedirectToAction(nameof(Index));
            }
            obj.WidgetContentCategoryList = _widgetContentCategoryService.GetAllWidgetContentCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });           
            TempData["error"] = "Create failed";
            return View(obj);
        }
        public IActionResult Update(int widgetContentId)
        {

            WidgetContentVM widgetContentVM = new()
            {
                WidgetContentCategoryList = _widgetContentCategoryService.GetAllWidgetContentCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                WidgetContent = _widgetContentService.GetWidgetContentById(widgetContentId)
            };
            if (widgetContentVM == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(widgetContentVM);
        }

        [HttpPost]
        public IActionResult Update(WidgetContentVM widgetContentVM)
        {
            widgetContentVM.WidgetContentCategoryList = _widgetContentCategoryService.GetAllWidgetContentCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            if (ModelState.IsValid && widgetContentVM.WidgetContent.Id > 0)
            {                        
                widgetContentVM.WidgetContent.IsActive = true;
                _widgetContentService.UpdateWidgetContent(widgetContentVM.WidgetContent);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Updated failed";
            return View(widgetContentVM);
        }
        public IActionResult Delete(int widgetContentId)
        {
            WidgetContentVM widgetContentVM = new()
            {
                WidgetContentCategoryList = _widgetContentCategoryService.GetAllWidgetContentCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                WidgetContent = _widgetContentService.GetWidgetContentById(widgetContentId)
            };
            if (widgetContentVM == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(widgetContentVM);
        }

        [HttpPost]
        public IActionResult Delete(WidgetContentVM widgetContentVM)
        {
            WidgetContent? objFromDb = _widgetContentService.GetWidgetContentById(widgetContentVM.WidgetContent.Id);
            if (objFromDb is not null)
            {
                _widgetContentService.DeleteWidgetContent(objFromDb.Id);

                TempData["success"] = "Deleted successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Deleted failed";
            return View();
        }

        public IActionResult ChangeStatusWidgetContent(int widgetContentId)
        {
            WidgetContent? widgetContent = _widgetContentService.GetWidgetContentById(widgetContentId);
            if (widgetContent is not null)
            {
                widgetContent.IsActive = !widgetContent.IsActive; //Toggle the status
                _widgetContentService.UpdateWidgetContent(widgetContent);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}