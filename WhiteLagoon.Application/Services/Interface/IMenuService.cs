using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Application.Services.Interface
{
    public interface IMenuService
    {
        IEnumerable<Menu> GetAllMenu();
        Menu GetMenuById(int id);
        void CreateMenu(Menu menu);
        void UpdateMenu(Menu menu);
        bool DeleteMenu(int id);
        IEnumerable<Menu> GetMenuByCategory(string menuCategory);
    }
}
