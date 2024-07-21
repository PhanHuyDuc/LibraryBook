using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Domain.Entities
{
    public class WidgetContent
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey(nameof(WidgetContentCategory))]
        public int WidgetContentCategoryId { get; set; }
        [ValidateNever]
        public WidgetContentCategory WidgetContentCategory { get; set; }
    }
}
