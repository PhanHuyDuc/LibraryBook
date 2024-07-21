using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Web.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly IMenuService _menuService;
        private readonly IMenuCategoryService _menuCategoryService;
        public MainMenuViewComponent(IMenuService menuService, IMenuCategoryService menuCategoryService)
        {
            _menuService = menuService;
            _menuCategoryService = menuCategoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string menuCategory = "Main Menu";
            var menu = _menuService.GetMenuByCategory(menuCategory);
            return View(menu);
        }
    }
}
