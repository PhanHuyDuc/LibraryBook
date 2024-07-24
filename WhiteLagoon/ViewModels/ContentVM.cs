using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Web.ViewModels
{
    public class ContentVM
    {
        public Content? Content { get; set; }       
        [ValidateNever]
        public IEnumerable<SelectListItem>? ContentCategoryList { get; set; }
       
    }
}
