using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Web.ViewModels
{
    public class MediaVM
    {
        public Media? Media { get; set; }       
        [ValidateNever]
        public IEnumerable<SelectListItem>? MediaCategoryList { get; set; }
       
    }
}
