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
    public class WidgetContentCategoryService : IWidgetContentCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WidgetContentCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void CreateWidgetContentCategory(WidgetContentCategory widgetContentCategory)
        {
            _unitOfWork.WidgetContentCategory.Add(widgetContentCategory);
            _unitOfWork.Save();
        }

        public bool DeleteWidgetContentCategory(int id)
        {
            try
            {
                WidgetContentCategory? objFromDb = _unitOfWork.WidgetContentCategory.Get(u => u.Id == id);
                if (objFromDb is not null)
                {

                    _unitOfWork.WidgetContentCategory.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<WidgetContentCategory> GetAllWidgetContentCategory()
        {
           return _unitOfWork.WidgetContentCategory.GetAll();
        }

        public WidgetContentCategory GetWidgetContentCategoryById(int id)
        {
            return _unitOfWork.WidgetContentCategory.Get(u=>u.Id == id);
        }

        public void UpdateWidgetContentCategory(WidgetContentCategory widgetContentCategory)
        {
            _unitOfWork.WidgetContentCategory.Update(widgetContentCategory);
            _unitOfWork.Save();
        }
    }
}
