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
    public class ContentCategoryRepository : Repository<ContentCategory>, IContentCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public ContentCategoryRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }



        public void Update(ContentCategory entity)
        {
            _db.ContentCategories.Update(entity);
        }
    }
}
