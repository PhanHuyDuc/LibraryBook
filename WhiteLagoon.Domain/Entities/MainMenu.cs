using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Domain.Entities
{
    public class MainMenu
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string MenuLink { get; set; }
        [ValidateNever]
        public IEnumerable<MenuCategory> MainMenuCategory { get; set; }
    }
}
