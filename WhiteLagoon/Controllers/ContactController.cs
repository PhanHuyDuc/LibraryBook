using LibraryBook.Application.Common.Utility;
using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Manager)]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public IActionResult Index()
        {
            var ContentCategories = _contactService.GetAllContact();
            return View(ContentCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Contact obj)
        {
            if (ModelState.IsValid)
            {

                _contactService.CreateContact(obj);
                TempData["success"] = "Create successfully";
                return RedirectToAction("contact", "Home");

            }
            TempData["error"] = "Create failed";
            return View();
        }
        [HttpPost]
        public IActionResult CreateSubs(Contact obj)
        {
            var emailExists = _contactService.GetAllContact().Any(u => u.Email == obj.Email);
            if (ModelState.IsValid && !emailExists)
            {
                _contactService.CreateContact(obj);
                TempData["success"] = "Subscribe successfully";
                return RedirectToAction("Index", "Home");

            }
            TempData["error"] = "Email already subscribe";
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Update(int contactId)
        {
            Contact? obj = _contactService.GetContactById(contactId);

            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Contact obj)
        {

            if (ModelState.IsValid && obj.Id > 0)
            {

                _contactService.UpdateContact(obj);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Updated failed";
            return View();
        }
        public IActionResult Delete(int contactId)
        {
            Contact? obj = _contactService.GetContactById(contactId);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Contact obj)
        {
            bool deleted = _contactService.DeleteContact(obj.Id);
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
