﻿using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Web.ViewComponents
{
    public class SpecialContentViewComponent : ViewComponent
    {
        private readonly IContentService _contentService;
        private readonly IContentCategoryService _contentCategoryService;
        public SpecialContentViewComponent(IContentService contentService, IContentCategoryService contentCategoryService)
        {
            _contentService = contentService;
            _contentCategoryService = contentCategoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            var content = _contentService.GetAllContent().Where(u=>u.IsSpecial==true).OrderByDescending(u=>u.Updated_Date).Take(2);
            return View(content);
        }
    }
}
