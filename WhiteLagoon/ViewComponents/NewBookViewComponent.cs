using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Web.ViewComponents
{
    public class NewBookViewComponent : ViewComponent
    {
        private readonly IContentService _contentService;
        private readonly IContentCategoryService _contentCategoryService;
        public NewBookViewComponent(IContentService contentService, IContentCategoryService contentCategoryService)
        {
            _contentService = contentService;
            _contentCategoryService = contentCategoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            var content = _contentService.GetAllContent().OrderByDescending(u=>u.Updated_Date).Take(4);
            return View(content);
        }
    }
}
