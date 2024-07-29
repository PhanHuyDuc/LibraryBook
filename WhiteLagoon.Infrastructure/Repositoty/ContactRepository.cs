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
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private readonly ApplicationDbContext _db;
        public ContactRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Contact entity)
        {
            _db.Contacts.Update(entity);
        }
    }
}
