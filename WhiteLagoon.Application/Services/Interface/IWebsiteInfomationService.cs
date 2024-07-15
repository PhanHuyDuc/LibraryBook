using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryBook.Domain.Entities;

namespace LibraryBook.Application.Services.Interface
{
    public interface IWebsiteInfomationService
    {
        IEnumerable<WebsiteInfomation> GetAllInfos();
        WebsiteInfomation GetInfoById(int id);
        void CreateWebsiteInfomation(WebsiteInfomation websiteInfomation);
        void UpdateWebsiteInfomation(WebsiteInfomation websiteInfomation);
        bool DeleteWebsiteInfomation(int id);

       
    }
}
