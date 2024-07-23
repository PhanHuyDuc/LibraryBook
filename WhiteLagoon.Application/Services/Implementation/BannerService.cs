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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BannerService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public void CreateBanner(Banner banner)
        {
            if (banner.BannerImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(banner.BannerImage.FileName);
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\BannerImage");

                using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                banner.BannerImage.CopyTo(fileStream);

                banner.BannerImageUrl = @"\images\BannerImage\" + fileName;
            }
            else
            {
                banner.BannerImageUrl = "https://placehold.co/600x400";
            }

            _unitOfWork.Banner.Add(banner);
            _unitOfWork.Save();
        }

        public bool DeleteBanner(int id)
        {
            try
            {
                Banner? objFromDb = _unitOfWork.Banner.Get(u => u.Id == id);
                if (objFromDb is not null)
                {
                    if (!string.IsNullOrEmpty(objFromDb.BannerImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.BannerImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
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

        public void UpdateBanner(Banner banner)
        {
            if (banner.BannerImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(banner.BannerImage.FileName);
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\BannerImage");

                if (!string.IsNullOrEmpty(banner.BannerImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, banner.BannerImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                banner.BannerImage.CopyTo(fileStream);
                banner.BannerImageUrl = @"\images\BannerImage\" + fileName;
            }
            _unitOfWork.Banner.Update(banner);
            _unitOfWork.Save();
        }
    }
}
