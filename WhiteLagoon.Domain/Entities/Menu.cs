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
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? MenuParentName { get; set; }
        public string MenuName { get; set; }
        public string? MenuLink { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("MenuCategory")]
        public int MenuCategoryId { get; set; }
        
        [ValidateNever]
        public MenuCategory MenuCategory { get; set; }
    }
}
