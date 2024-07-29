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
    public class BannerCategoryService : IBannerCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BannerCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void CreateBannerCategory(BannerCategory bannerCategory)
        {
            _unitOfWork.BannerCategory.Add(bannerCategory);
            _unitOfWork.Save();
        }

        public bool DeleteBannerCategory(int id)
        {
            try
            {
                BannerCategory? objFromDb = _unitOfWork.BannerCategory.Get(u => u.Id == id);
                if (objFromDb is not null)
                {

                    _unitOfWork.BannerCategory.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<BannerCategory> GetAllBannerCategory()
        {
           return _unitOfWork.BannerCategory.GetAll();
        }

        public BannerCategory GetBannerCategoryById(int id)
        {
            return _unitOfWork.BannerCategory.Get(u=>u.Id == id);
        }

        public void UpdateBannerCategory(BannerCategory bannerCategory)
        {
            _unitOfWork.BannerCategory.Update(bannerCategory);
            _unitOfWork.Save();
        }
    }
}
