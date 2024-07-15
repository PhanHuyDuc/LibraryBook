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

namespace LibraryBook.Application.Services.Implementation
{
    public class WebsiteInfomationService : IWebsiteInfomationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WebsiteInfomationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void CreateWebsiteInfomation(WebsiteInfomation websiteInfomation)
        {
            _unitOfWork.WebsiteInfomation.Add(websiteInfomation);
            _unitOfWork.Save();
        }

        public bool DeleteWebsiteInfomation(int id)
        {
            try
            {
                WebsiteInfomation? objFromDb = _unitOfWork.WebsiteInfomation.Get(u => u.Id == id);
                if (objFromDb is not null)
                {

                    _unitOfWork.WebsiteInfomation.Remove(objFromDb);
                    _unitOfWork.Save();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<WebsiteInfomation> GetAllInfos()
        {
           return _unitOfWork.WebsiteInfomation.GetAll();
        }

        public WebsiteInfomation GetInfoById(int id)
        {
            return _unitOfWork.WebsiteInfomation.Get(u=>u.Id == id);
        }

        public void UpdateWebsiteInfomation(WebsiteInfomation websiteInfomation)
        {
            _unitOfWork.WebsiteInfomation.Update(websiteInfomation);
            _unitOfWork.Save();
        }
    }
}
