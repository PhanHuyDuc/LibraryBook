using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Web.ViewModels
{
    public class MenuVM
    {
        public Menu? Menu { get; set; }       
        [ValidateNever]
        public IEnumerable<SelectListItem>? MenuCategoryList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? MenuParentList { get; set; }
    }
}
