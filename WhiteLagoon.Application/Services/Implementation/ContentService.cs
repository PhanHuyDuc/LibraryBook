﻿using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Application.Common.Interfaces;
using LibraryBook.Application.Common.Utility;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace LibraryBook.Application.Services.Implementation
{
    public class ContentService : IContentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ContentService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public void CreateContent(Content content)
        {
            string wwwRoothPath = _webHostEnvironment.WebRootPath;
            if (content.Avata != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(content.Avata.FileName);
                string imagePath = Path.Combine(wwwRoothPath, @"images\ContentImage");

                using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                content.Avata.CopyTo(fileStream);

                content.ContentAvata = @"\images\ContentImage\" + fileName;
            }
            else
            {
                content.ContentAvata = "https://placehold.co/600x400";
            }
            if (content.files != null)
            {
                foreach (IFormFile multifile in content.files)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(multifile.FileName);
                    string contentPath = @"images\ContentImage\content-" + content.Id;
                    string finalPath = Path.Combine(wwwRoothPath, contentPath);

                    if (!Directory.Exists(finalPath))
                        Directory.CreateDirectory(finalPath);

                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        multifile.CopyTo(fileStream);
                    }

                    ContentImage contentImage = new()
                    {
                        MultiImage = @"\" + contentPath + @"\" + fileName,
                        ContentId = content.Id,
                    };

                    if (content.ContentImage == null)
                        content.ContentImage = new List<ContentImage>();

                    content.ContentImage.Add(contentImage);
                }
            }
            _unitOfWork.Content.Add(content);
            _unitOfWork.Save();
        }       

        public bool DeleteContent(int id)
        {
            try
            {
                Content? objFromDb = _unitOfWork.Content.Get(u => u.Id == id);
                if (objFromDb is not null)
                {
                    if (!string.IsNullOrEmpty(objFromDb.ContentAvata))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.ContentAvata.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    _unitOfWork.Content.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Content> GetAllContent()
        {
            return _unitOfWork.Content.GetAll(includeProperties: "ContentCategory");
        }

        public IEnumerable<Content> GetContentByCategory(string contentCategory)
        {
            return _unitOfWork.Content.GetAll(includeProperties: "ContentCategory").Where(u => u.ContentCategory.Name == contentCategory);
        }

        public Content GetContentById(int id)
        {
            return _unitOfWork.Content.Get(u => u.Id == id, includeProperties: "ContentCategory");
        }

        public void UpdateContent(Content content)
        {
            if (content.Avata != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(content.Avata.FileName);
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\ContentImage");

                if (!string.IsNullOrEmpty(content.ContentAvata))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, content.ContentAvata.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                content.Avata.CopyTo(fileStream);
                content.ContentAvata = @"\images\ContentImage\" + fileName;
            }
            _unitOfWork.Content.Update(content);
            _unitOfWork.Save();
        }
    }
}
