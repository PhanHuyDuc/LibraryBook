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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            if (content.Updated_Date == null)
            {
                content.Updated_Date = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.Content.Add(content);
            _unitOfWork.Save();
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
                _unitOfWork.Content.Update(content);
                _unitOfWork.Save();
            }

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

                    string contentPath = @"images/ContentImage/content-" + id;
                    string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, contentPath);

                    if (Directory.Exists(finalPath))
                    {
                        string[] filePaths = Directory.GetFiles(finalPath);
                        foreach (string filePath in filePaths)
                        {
                            System.IO.File.Delete(filePath);
                        }

                        Directory.Delete(finalPath);

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
            return _unitOfWork.Content.Get(u => u.Id == id, includeProperties: "ContentCategory, ContentImage");
        }
        public void UpdateContent(Content content)
        {
            string wwwRoothPath = _webHostEnvironment.WebRootPath;
            if (content.Avata != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(content.Avata.FileName);
                string imagePath = Path.Combine(wwwRoothPath, @"images\ContentImage");

                if (!string.IsNullOrEmpty(content.ContentAvata))
                {
                    var oldImagePath = Path.Combine(wwwRoothPath, content.ContentAvata.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                content.Avata.CopyTo(fileStream);
                content.ContentAvata = @"\images\ContentImage\" + fileName;
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
            _unitOfWork.Content.Update(content);
            _unitOfWork.Save();
        }
        public void DeleteImage(int imageId)
        {
            var imageToBeDelete = _unitOfWork.ContentImage.Get(u => u.Id == imageId);
            var contentId = imageToBeDelete.ContentId;
            if (imageToBeDelete != null)
            {
                if (!string.IsNullOrEmpty(imageToBeDelete.MultiImage))
                {
                    //delete old image
                    var oldImagePath =
                        Path.Combine(_webHostEnvironment.WebRootPath,
                        imageToBeDelete.MultiImage.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.ContentImage.Remove(imageToBeDelete);
                _unitOfWork.Save();
            }
        }

        public Pagination SearchContent(string search, int pageNumber, int pageSize)
        {

            if (!string.IsNullOrEmpty(search))
            {
                var searchString = search.Trim().ToLower();
                var contentList = _unitOfWork.Content.GetAll(includeProperties: "ContentCategory");
                contentList = contentList.Where(u => u.Name.ToLower().Contains(searchString) ||
                                                u.Description.ToLower().Contains(searchString) ||
                                                u.Author.ToLower().Contains(searchString)|| u.ContentCategory.Name.ToLower().Contains(searchString));
                var contentResult = contentList.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                Pagination pagination = new()
                {
                    Content = contentResult,
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalRecords = contentList.Count(),
                    SearchString = searchString,
                };
                return pagination;
            }
            else
            {
                var contentList = _unitOfWork.Content.GetAll(includeProperties: "ContentCategory").Skip((pageNumber - 1) * pageSize).Take(pageSize);
                Pagination pagination = new()
                {
                    Content = contentList,
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalRecords = _unitOfWork.Content.GetAll().Count(),
                };
                return pagination;
            }


        }

        public Pagination GetAllContentPagination(int pageNumber, int pageSize)
        {

            var contentList = _unitOfWork.Content.GetAll(includeProperties: "ContentCategory").Skip((pageNumber - 1) * pageSize).Take(pageSize);
            Pagination pagination = new()
            {
                Content = contentList,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalRecords = _unitOfWork.Content.GetAll().Count(),
            };
            return pagination;
        }

        public Pagination GetContentPaginationByCategory(int pageNumber, int pageSize, int ContentCat)
        {
            var contentList = _unitOfWork.Content.GetAll(includeProperties: "ContentCategory").Where(u => u.ContentCategoryId == ContentCat).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var contentCat = _unitOfWork.ContentCategory.Get(u => u.Id == ContentCat);
            Pagination pagination = new()
            {
                Content = contentList,
                ContentCategory = contentCat,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalRecords = _unitOfWork.Content.GetAll(includeProperties: "ContentCategory").Where(u => u.ContentCategoryId == ContentCat).Count(),
            };
            return pagination;
        }

        public ContentDetail GetContentDetail(int id)
        {
            var contents = _unitOfWork.Content.Get(u => u.Id == id, includeProperties: "ContentCategory, ContentImage");
            var relatedContents = _unitOfWork.Content.GetAll().Where(u => u.ContentCategoryId == contents.ContentCategoryId);
            ContentDetail contentDetail = new()
            {
                Content = contents,
                RelatedContent = relatedContents,
            };
            return contentDetail;
        }
    }
}
