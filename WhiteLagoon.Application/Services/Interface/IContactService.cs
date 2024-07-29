using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace LibraryBook.Application.Services.Interface
{
    public interface IContactService
    {
        IEnumerable<Contact> GetAllContact();
        Contact GetContactById(int id);
        void CreateContact(Contact Contact);
        void UpdateContact(Contact Contact);
        bool DeleteContact(int id);       
    }
}
