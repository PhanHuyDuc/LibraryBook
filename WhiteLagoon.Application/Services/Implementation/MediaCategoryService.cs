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
    public class MediaCategoryService : IMediaCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MediaCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void CreateMediaCategory(MediaCategory mediaCategory)
        {
            _unitOfWork.MediaCategory.Add(mediaCategory);
            _unitOfWork.Save();
        }

        public bool DeleteMediaCategory(int id)
        {
            try
            {
                MediaCategory? objFromDb = _unitOfWork.MediaCategory.Get(u => u.Id == id);
                if (objFromDb is not null)
                {

                    _unitOfWork.MediaCategory.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<MediaCategory> GetAllMediaCategory()
        {
           return _unitOfWork.MediaCategory.GetAll();
        }

        public MediaCategory GetMediaCategoryById(int id)
        {
            return _unitOfWork.MediaCategory.Get(u=>u.Id == id);
        }

        public void UpdateMediaCategory(MediaCategory mediaCategory)
        {
            _unitOfWork.MediaCategory.Update(mediaCategory);
            _unitOfWork.Save();
        }
    }
}
