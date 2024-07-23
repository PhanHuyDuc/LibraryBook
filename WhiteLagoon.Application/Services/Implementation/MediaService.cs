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
    public class MediaService : IMediaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MediaService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public void CreateMedia(Media media)
        {
            if (media.MediaImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(media.MediaImage.FileName);
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"media");

                using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                media.MediaImage.CopyTo(fileStream);

                media.MediaImageUrl = @"media\" + fileName;
            }
            else
            {
                media.MediaImageUrl = "https://placehold.co/600x400";
            }

            _unitOfWork.Media.Add(media);
            _unitOfWork.Save();
        }

        public bool DeleteMedia(int id)
        {
            try
            {
                Media? objFromDb = _unitOfWork.Media.Get(u => u.Id == id);
                if (objFromDb is not null)
                {
                    if (!string.IsNullOrEmpty(objFromDb.MediaImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.MediaImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    _unitOfWork.Media.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Media> GetAllMedia()
        {
           return _unitOfWork.Media.GetAll(includeProperties:"MediaCategory");
        }

        public IEnumerable<Media> GetMediaByCategory(string mediaCategory)
        {
            return _unitOfWork.Media.GetAll(includeProperties: "MediaCategory").Where(u=>u.MediaCategory.Name == mediaCategory);
        }

        public Media GetMediaById(int id)
        {
            return _unitOfWork.Media.Get(u=>u.Id == id, includeProperties: "MediaCategory");
        }

        public void UpdateMedia(Media media)
        {
            if (media.MediaImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(media.MediaImage.FileName);
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"media");

                if (!string.IsNullOrEmpty(media.MediaImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, media.MediaImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                media.MediaImage.CopyTo(fileStream);
                media.MediaImageUrl = @"\media\" + fileName;
            }
            _unitOfWork.Media.Update(media);
            _unitOfWork.Save();
        }
    }
}
