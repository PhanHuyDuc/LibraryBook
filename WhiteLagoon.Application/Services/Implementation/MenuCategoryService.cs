using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Application.Common.Interfaces;
using LibraryBook.Application.Common.Utility;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Application.Services.Implementation
{
    public class MenuCategoryService : IMenuCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void CreateMenuCategory(MenuCategory menuCategory)
        {
            _unitOfWork.MenuCategory.Add(menuCategory);
            _unitOfWork.Save();
        }

        public bool DeleteMenuCategory(int id)
        {
            try
            {
                MenuCategory? objFromDb = _unitOfWork.MenuCategory.Get(u => u.Id == id);
                if (objFromDb is not null)
                {

                    _unitOfWork.MenuCategory.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<MenuCategory> GetAllMenuCategory()
        {
           return _unitOfWork.MenuCategory.GetAll();
        }

        public MenuCategory GetMenuCategoryById(int id)
        {
            return _unitOfWork.MenuCategory.Get(u=>u.Id == id);
        }

        public void UpdateMenuCategory(MenuCategory menuCategory)
        {
            _unitOfWork.MenuCategory.Update(menuCategory);
            _unitOfWork.Save();
        }
    }
}
