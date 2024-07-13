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
    public class SubSubMenu
    {
        [Key]
        public int Id { get; set; }
        public string SubSubMenuName { get; set; }
        public string SubSubMenuLink { get; set; }
        [ForeignKey(nameof(SubMenu))]
        public int SubMenuId { get; set; }
        [ValidateNever]
        public SubMenu SubMenu { get; set; }
    }
}
