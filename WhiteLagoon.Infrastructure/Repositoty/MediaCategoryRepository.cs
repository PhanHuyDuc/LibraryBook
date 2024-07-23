using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Application.Common.Interfaces;
using LibraryBook.Domain.Entities;
using LibraryBook.Infrastructure.Data;

namespace LibraryBook.Infrastructure.Repositoty
{
    public class MediaCategoryRepository : Repository<MediaCategory>, IMediaCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public MediaCategoryRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }



        public void Update(MediaCategory entity)
        {
            _db.MediaCategories.Update(entity);
        }
    }
}
