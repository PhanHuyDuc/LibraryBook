using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Web.ViewModels
{
    public class BannerVM
    {
        public Banner? Banner { get; set; }       
        [ValidateNever]
        public IEnumerable<SelectListItem>? BannerCategoryList { get; set; }
       
    }
}
