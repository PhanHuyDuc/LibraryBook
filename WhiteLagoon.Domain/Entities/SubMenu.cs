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
    public class SubMenu
    {
        [Key]
        public int Id { get; set; }
        public string SubMenuName { get; set; }
        public string SubMenuLink { get; set; }
        [ForeignKey(nameof(MainMenu))]
        public int MainMenuId { get; set; }
        [ValidateNever]
        public MainMenu MainMenu { get; set; }
    }
}
