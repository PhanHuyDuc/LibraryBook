using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Application.Services.Interface
{
    public interface IContentCategoryService
    {
        IEnumerable<ContentCategory> GetAllContentCategory();
        ContentCategory GetContentCategoryById(int id);
        void CreateContentCategory(ContentCategory contentCategory);
        void UpdateContentCategory(ContentCategory contentCategory);
        bool DeleteContentCategory(int id);
    }
}
