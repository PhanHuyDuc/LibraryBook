using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Web.Controllers
{
    public class WebsiteInfomationController : Controller
    {
        private readonly IWebsiteInfomationService _websiteInfomationService;
        public WebsiteInfomationController(IWebsiteInfomationService websiteInfomationService)
        {
            _websiteInfomationService = websiteInfomationService;
        }
        // GET: WebsiteInfomationController
        public IActionResult Index()
        {
            var websiteInfo = _websiteInfomationService.GetAllInfos();
            return View(websiteInfo);
        }

        public IActionResult Update(int websiteInfomationId)
        {
            WebsiteInfomation? obj = _websiteInfomationService.GetInfoById(websiteInfomationId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        // POST: WebsiteInfomationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(WebsiteInfomation websiteInfomation)
        {
            if (ModelState.IsValid && websiteInfomation.Id > 0)
            {

                _websiteInfomationService.UpdateWebsiteInfomation(websiteInfomation);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Updated failed";
            return View();
        }
    }
}
