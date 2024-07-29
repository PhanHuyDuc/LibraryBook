using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Application.Common.Interfaces;
using LibraryBook.Application.Common.Utility;
using LibraryBook.Application.Services.Interface;
using LibraryBook.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBook.Application.Services.Implementation
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }

        public void CreateContact(Contact contact)
        {            
            _unitOfWork.Contact.Add(contact);
            _unitOfWork.Save();          
            
        }       

        public bool DeleteContact(int id)
        {
            try
            {
                Contact? objFromDb = _unitOfWork.Contact.Get(u => u.Id == id);
                if (objFromDb is not null)
                {
                   
                    _unitOfWork.Contact.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Contact> GetAllContact()
        {
            return _unitOfWork.Contact.GetAll();
        }



        public Contact GetContactById(int id)
        {
            return _unitOfWork.Contact.Get(u => u.Id == id);
        }
        public void UpdateContact(Contact contact)
        {
            _unitOfWork.Contact.Update(contact);
            _unitOfWork.Save();
        }
    }
}
