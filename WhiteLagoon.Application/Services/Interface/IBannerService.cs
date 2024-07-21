using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Application.Services.Interface
{
    public interface IBannerService
    {
        IEnumerable<Banner> GetAllBanner();
        Banner GetBannerById(int id);
        void CreateBanner(Banner Banner);
        void UpdateBanner(Banner Banner);
        bool DeleteBanner(int id);
        IEnumerable<Banner> GetBannerByCategory(string BannerCategory);
    }
}
