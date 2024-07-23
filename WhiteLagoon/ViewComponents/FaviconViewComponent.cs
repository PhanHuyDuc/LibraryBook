using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Web.ViewComponents
{
    public class FaviconViewComponent : ViewComponent
    {
        private readonly IBannerService _bannerService;
        private readonly IBannerCategoryService _bannerCategoryService;
        public FaviconViewComponent(IBannerService bannerService, IBannerCategoryService bannerCategoryService)
        {
            _bannerService = bannerService;
            _bannerCategoryService = bannerCategoryService;            
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string bannerCategory = "Favicon";
            var banner = _bannerService.GetBannerByCategory(bannerCategory);
            return View(banner);
        }
    }
}
