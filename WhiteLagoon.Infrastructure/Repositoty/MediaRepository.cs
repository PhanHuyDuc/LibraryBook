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
    public class MediaRepository : Repository<Media>, IMediaRepository
    {
        private readonly ApplicationDbContext _db;
        public MediaRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Media entity)
        {
            _db.Medias.Update(entity);
        }
    }
}
