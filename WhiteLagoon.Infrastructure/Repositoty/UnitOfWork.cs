using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Application.Common.Interfaces;
using LibraryBook.Infrastructure.Data;

namespace LibraryBook.Infrastructure.Repositoty
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IVillaRepository Villa { get; private set; }

        public IVillaNumberRepository VillaNumber { get; private set; }

        public IAmenityRepository Amenity { get; private set; }
        public IBookingRepository Booking { get; private set; }
        public IApplicationUserRepository User { get; private set; }
        public IWebsiteInfomationRepository WebsiteInfomation { get; private set; }
        public IMenuRepository Menu { get; private set; }
        public IBannerRepository Banner { get; private set; }
        public IMenuCategoryRepository MenuCategory { get; private set; }
        public IBannerCategoryRepository BannerCategory { get; private set; }
        public IMediaRepository Media { get; private set; }
        public IMediaCategoryRepository MediaCategory { get; private set; }
        public IContentCategoryRepository ContentCategory { get; private set; }
        public IContentRepository Content { get; private set; }
        public IContactRepository Contact { get; private set; }
        public IContentImageRepository ContentImage { get; private set; }
        public IWidgetContentRepository WidgetContent { get; private set; }
        public IWidgetContentCategoryRepository WidgetContentCategory { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Villa = new VillaRepository(_db);
            VillaNumber = new VillaNumberRepository(_db);
            Amenity = new AmenityRepository(_db);
            Booking = new BookingRepository(_db);
            User = new ApplicationUserRepository(_db);
            WebsiteInfomation = new WebsiteInfomationRepository(_db);
            Menu = new MenuRepository(_db);
            MenuCategory = new MenuCategoryRepository(_db);
            Banner = new BannerRepository(_db);
            BannerCategory = new BannerCategoryRepository(_db);
            Media = new MediaRepository(_db);
            MediaCategory = new MediaCategoryRepository(_db);
            ContentCategory = new ContentCategoryRepository(_db);
            Content = new ContentRepository(_db);
            Contact = new ContactRepository(_db);
            ContentImage = new ContentImageRepository(_db);
            WidgetContent = new WidgetContentRepository(_db);
            WidgetContentCategory = new WidgetContentCategoryRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
