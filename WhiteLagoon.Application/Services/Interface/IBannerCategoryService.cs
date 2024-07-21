using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Application.Services.Interface
{
    public interface IBannerCategoryService
    {
        IEnumerable<BannerCategory> GetAllBannerCategory();
        BannerCategory GetBannerCategoryById(int id);
        void CreateBannerCategory(BannerCategory BannerCategory);
        void UpdateBannerCategory(BannerCategory BannerCategory);
        bool DeleteBannerCategory(int id);
    }
}
