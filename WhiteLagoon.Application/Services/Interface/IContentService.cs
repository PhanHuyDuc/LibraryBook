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
        void CreateContent(Content content);
        void UpdateContent(Content content);
        bool DeleteContent(int id);
        IEnumerable<Content> GetContentByCategory(string contentCategory);
        void DeleteImage(int imageId);
    }
}
