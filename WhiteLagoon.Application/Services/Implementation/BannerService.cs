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
    public class BannerService : IBannerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BannerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void CreateBanner(Banner Banner)
        {
            _unitOfWork.Banner.Add(Banner);
            _unitOfWork.Save();
        }

        public bool DeleteBanner(int id)
        {
            try
            {
                Banner? objFromDb = _unitOfWork.Banner.Get(u => u.Id == id);
                if (objFromDb is not null)
                {

                    _unitOfWork.Banner.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Banner> GetAllBanner()
        {
           return _unitOfWork.Banner.GetAll(includeProperties:"BannerCategory");
        }

        public IEnumerable<Banner> GetBannerByCategory(string BannerCategory)
        {
            return _unitOfWork.Banner.GetAll(includeProperties: "BannerCategory").Where(u=>u.BannerCategory.Name == BannerCategory);
        }

        public Banner GetBannerById(int id)
        {
            return _unitOfWork.Banner.Get(u=>u.Id == id, includeProperties: "BannerCategory");
        }

        public void UpdateBanner(Banner Banner)
        {
            _unitOfWork.Banner.Update(Banner);
            _unitOfWork.Save();
        }
    }
}
