using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Web.ViewModels
{
    public class WidgetContentVM
    {
        public WidgetContent? WidgetContent { get; set; }       
        [ValidateNever]
        public IEnumerable<SelectListItem>? WidgetContentCategoryList { get; set; }
       
    }
}
