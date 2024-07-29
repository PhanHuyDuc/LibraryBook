using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Web.ViewComponents
{
    public class ContactViewComponent : ViewComponent
    {
        private readonly IWidgetContentService _widgetContentService;
        private readonly IWidgetContentCategoryService _widgetContentCategoryService;
        public ContactViewComponent(IWidgetContentService widgetContentService, IWidgetContentCategoryService widgetContentCategoryService)
        {
            _widgetContentService = widgetContentService;
            _widgetContentCategoryService = widgetContentCategoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string wCat = "Contact";
            var widgetContent = _widgetContentService.GetWidgetContentByCategory(wCat);
            return View(widgetContent);
        }
    }
}
