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
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void CreateMenu(Menu menu)
        {
            _unitOfWork.Menu.Add(menu);
            _unitOfWork.Save();
        }

        public bool DeleteMenu(int id)
        {
            try
            {
                Menu? objFromDb = _unitOfWork.Menu.Get(u => u.Id == id);
                if (objFromDb is not null)
                {

                    _unitOfWork.Menu.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Menu> GetAllMenu()
        {
           return _unitOfWork.Menu.GetAll();
        }

        public Menu GetMenuById(int id)
        {
            return _unitOfWork.Menu.Get(u=>u.Id == id);
        }

        public void UpdateMenu(Menu menu)
        {
            _unitOfWork.Menu.Update(menu);
            _unitOfWork.Save();
        }
    }
}
