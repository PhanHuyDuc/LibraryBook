using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryBook.Application.Common.Interfaces;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using LibraryBook.Infrastructure.Data;
using LibraryBook.Infrastructure.Repositoty;
using LibraryBook.Application.Common.Utility;

namespace LibraryBook.Web.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Manager)]
    public class VillaController : Controller
    {
       private readonly IVillaService _villaService;
        public VillaController(IVillaService villaService)
        {
            _villaService = villaService;            
        }
        public IActionResult Index()
        {
            var villas = _villaService.GetAllVillas();
            return View(villas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", "The description can not be match with Name");
            }
            if (ModelState.IsValid)
            {

                _villaService.CreateVilla(obj);
                TempData["success"] = "Create successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Create failed";
            return View();
        }
        public IActionResult Update(int villaId)
        {
            Villa? obj = _villaService.GetVillaById(villaId);

            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {

            if (ModelState.IsValid && obj.Id > 0)
            {

               _villaService.UpdateVilla(obj);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Updated failed";
            return View();
        }
        public IActionResult Delete(int villaId)
        {
            Villa? obj = _villaService.GetVillaById(villaId);

            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            bool deleted = _villaService.DeleteVilla(obj.Id);
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
