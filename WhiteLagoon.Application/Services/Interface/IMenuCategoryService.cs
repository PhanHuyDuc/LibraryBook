using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Application.Services.Interface
{
    public interface IMenuCategoryService
    {
        IEnumerable<MenuCategory> GetAllMenuCategory();
        MenuCategory GetMenuCategoryById(int id);
        void CreateMenuCategory(MenuCategory menuCategory);
        void UpdateMenuCategory(MenuCategory menuCategory);
        bool DeleteMenuCategory(int id);
    }
}
