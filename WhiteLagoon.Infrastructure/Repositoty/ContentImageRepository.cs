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
    public class ContentImageRepository : Repository<ContentImage>, IContentImageRepository
    {
        private readonly ApplicationDbContext _db;
        public ContentImageRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(ContentImage entity)
        {
            _db.ContentImages.Update(entity);
        }
    }
}
