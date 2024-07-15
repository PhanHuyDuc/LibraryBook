using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Domain.Entities
{
    public class WebsiteInfomation
    {
        [Key]
        public int Id { get; set; }
        public string WebsiteName { get; set; }
        public string WebsiteAddress { get; set; }
        public string WebsitePhoneNumber { get; set; }
        public string? WebsitePhoneNumber2 { get; set; }
        public string? WebsitePhoneNumber3 { get; set; }
        public string? WebsiteEmail { get; set; }
        public string? WebsiteEmail2 { get; set; }
        public string? WebsiteEmail3 { get; set; }
        public string? Fax { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? WebsiteTitle { get; set; }
        public string? WebsiteAdminTitle { get;set; }

    }
}
