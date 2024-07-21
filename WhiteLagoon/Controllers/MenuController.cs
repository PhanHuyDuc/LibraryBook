using LibraryBook.Application.Services.Implementation;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using LibraryBook.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace LibraryBook.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IMenuCategoryService _menuCategoryService;
        public MenuController(IMenuService menuService, IMenuCategoryService menuCategoryService)
        {
            _menuService = menuService;
            _menuCategoryService = menuCategoryService;
        }
        public IActionResult Index()
        {
            var menus = _menuService.GetAllMenu().OrderBy(u => u.MenuParentName);
            return View(menus);
        }
        public IActionResult Create()
        {
            MenuVM menuVM = new()
            {
                MenuCategoryList = _menuCategoryService.GetAllMenuCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                MenuParentList = _menuService.GetAllMenu().Select(u =>
                {

                    var text = u.MenuName;
                    if (u.ParentId != null)
                    {
                        text = "-" + text;
                    }
                    return new SelectListItem
                    {
                        Text = text,
                        Value = u.Id.ToString(),

                    };
                }),

            };
            return View(menuVM);
        }
        [HttpPost]
        public IActionResult Create(MenuVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Menu.ParentId != null)
                {
                    var parentMenu = _menuService.GetAllMenu().FirstOrDefault(u => u.Id == obj.Menu.ParentId);
                    if (parentMenu != null)
                    {
                        obj.Menu.MenuParentName = parentMenu.MenuName;
                        obj.Menu.TreeView = parentMenu.TreeView + 1;
                    }
                }
                else
                {
                    obj.Menu.TreeView = 1;
                }
                obj.Menu.IsActive = true;
                _menuService.CreateMenu(obj.Menu);
                TempData["success"] = "Create successfully";
                return RedirectToAction(nameof(Index));
            }
            obj.MenuCategoryList = _menuCategoryService.GetAllMenuCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            obj.MenuParentList = _menuService.GetAllMenu().Select(u =>
            {
                var text = u.MenuName;
                if (u.ParentId != null)
                {
                    text = "- " + text;
                }
                else if (true)
                {

                }
                return new SelectListItem
                {
                    Text = text,
                    Value = u.Id.ToString(),
                };
            });
            TempData["error"] = "Create failed";
            return View(obj);
        }
        public IActionResult Update(int menuId)
        {

            MenuVM menuVM = new()
            {
                MenuCategoryList = _menuCategoryService.GetAllMenuCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                MenuParentList = _menuService.GetAllMenu().Select(u =>
                {

                    var text = u.MenuName;
                    if (u.ParentId != null)
                    {
                        text = "-" + text;
                    }
                    return new SelectListItem
                    {
                        Text = text,
                        Value = u.Id.ToString(),

                    };
                }),
                Menu = _menuService.GetMenuById(menuId)
            };
            if (menuVM == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(menuVM);
        }

        [HttpPost]
        public IActionResult Update(MenuVM menuVM)
        {
            menuVM.MenuCategoryList = _menuCategoryService.GetAllMenuCategory().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            menuVM.MenuParentList = _menuService.GetAllMenu().Select(u =>
            {

                var text = u.MenuName;
                if (u.ParentId != null)
                {
                    text = "-" + text;
                }
                return new SelectListItem
                {
                    Text = text,
                    Value = u.Id.ToString(),

                };
            });

            if (ModelState.IsValid && menuVM.Menu.Id > 0)
            {
                if (menuVM.Menu.ParentId != null)
                {
                    var parentMenu = _menuService.GetAllMenu().FirstOrDefault(u => u.Id == menuVM.Menu.ParentId);
                    if (parentMenu != null)
                    {
                        menuVM.Menu.MenuParentName = parentMenu.MenuName;
                        menuVM.Menu.TreeView = parentMenu.TreeView + 1;
                    }
                    else
                    {
                        menuVM.Menu.TreeView = 1;
                    }
                }
                menuVM.Menu.IsActive = true;
                _menuService.UpdateMenu(menuVM.Menu);
                TempData["success"] = "Updated successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Updated failed";
            return View(menuVM);
        }
        public IActionResult Delete(int menuId)
        {
            MenuVM menuVM = new()
            {
                MenuCategoryList = _menuCategoryService.GetAllMenuCategory().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                MenuParentList = _menuService.GetAllMenu().Select(u =>
                {

                    var text = u.MenuName;
                    if (u.ParentId != null)
                    {
                        text = "-" + text;
                    }
                    return new SelectListItem
                    {
                        Text = text,
                        Value = u.Id.ToString(),

                    };
                }),
                Menu = _menuService.GetMenuById(menuId)
            };
            if (menuVM == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(menuVM);
        }

        [HttpPost]
        public IActionResult Delete(MenuVM menuVM)
        {
            Menu? objFromDb = _menuService.GetMenuById(menuVM.Menu.Id);
            if (objFromDb is not null)
            {
                _menuService.DeleteMenu(objFromDb.Id);

                TempData["success"] = "Deleted successfully";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "Deleted failed";
            return View();
        }

        public IActionResult ChangeStatusMenu(int menuId)
        {
            Menu? menu = _menuService.GetMenuById(menuId);
            if (menu is not null)
            {
                menu.IsActive = !menu.IsActive; //Toggle the status
                _menuService.UpdateMenu(menu);
            }
            return RedirectToAction(nameof(Index));
        }

        #region API Calls
        [HttpGet]
        [Authorize]
        public IActionResult GetMenuDataJson()
        {
            IEnumerable<Menu> objMenus;
            objMenus = _menuService.GetAllMenu();
            return Json(new { data = objMenus });
        }
        #endregion
    }
}