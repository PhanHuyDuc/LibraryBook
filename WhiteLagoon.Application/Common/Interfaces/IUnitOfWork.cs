using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IVillaRepository Villa { get; }
        IVillaNumberRepository VillaNumber { get; }
        IAmenityRepository Amenity { get; }
        IBookingRepository Booking { get; }
        IApplicationUserRepository User { get; }
        IWebsiteInfomationRepository WebsiteInfomation { get; }
        IMenuRepository Menu { get; }
        IMenuCategoryRepository MenuCategory { get; }
        IBannerRepository Banner { get; }
        IBannerCategoryRepository BannerCategory { get; }
        IMediaRepository Media { get; }
        IMediaCategoryRepository MediaCategory { get; }
        IContentCategoryRepository ContentCategory { get; }
        IContentRepository Content { get; }
        IContentImageRepository ContentImage { get; }
        IWidgetContentRepository WidgetContent { get; }
        IWidgetContentCategoryRepository WidgetContentCategory { get; }
        void Save();
    }
}
