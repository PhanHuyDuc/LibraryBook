using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Application.Services.Interface
{
    public interface IMediaCategoryService
    {
        IEnumerable<MediaCategory> GetAllMediaCategory();
        MediaCategory GetMediaCategoryById(int id);
        void CreateMediaCategory(MediaCategory mediaCategory);
        void UpdateMediaCategory(MediaCategory mediaCategory);
        bool DeleteMediaCategory(int id);
    }
}
