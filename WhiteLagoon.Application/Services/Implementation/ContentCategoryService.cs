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
    public class ContentCategoryService : IContentCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContentCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void CreateContentCategory(ContentCategory ContentCategory)
        {
            _unitOfWork.ContentCategory.Add(ContentCategory);
            _unitOfWork.Save();
        }

        public bool DeleteContentCategory(int id)
        {
            try
            {
                ContentCategory? objFromDb = _unitOfWork.ContentCategory.Get(u => u.Id == id);
                if (objFromDb is not null)
                {

                    _unitOfWork.ContentCategory.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ContentCategory> GetAllContentCategory()
        {
           return _unitOfWork.ContentCategory.GetAll();
        }

        public ContentCategory GetContentCategoryById(int id)
        {
            return _unitOfWork.ContentCategory.Get(u=>u.Id == id);
        }

        public void UpdateContentCategory(ContentCategory contentCategory)
        {
            _unitOfWork.ContentCategory.Update(contentCategory);
            _unitOfWork.Save();
        }
    }
}
