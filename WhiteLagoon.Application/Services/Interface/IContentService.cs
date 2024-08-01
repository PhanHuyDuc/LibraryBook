using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace LibraryBook.Application.Services.Interface
{
    public interface IContentService
    {
        IEnumerable<Content> GetAllContent();
        Content GetContentById(int id);
        ContentDetail GetContentDetail(int id);
        void CreateContent(Content content);
        void UpdateContent(Content content);
        bool DeleteContent(int id);
        IEnumerable<Content> GetContentByCategory(string contentCategory);
        void DeleteImage(int imageId);
        Pagination SearchContent(string searchString, int pageNumber, int pageSize);
        Pagination GetAllContentPagination(int pageNumber, int pageSize);
        Pagination GetContentPaginationByCategory(int pageNumber, int pageSize,int ContentCat);


    }
}
