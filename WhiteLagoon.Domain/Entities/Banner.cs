using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Domain.Entities
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public IFormFile? BannerImage { get; set; }
        public string? BannerImageUrl { get; set; }
        public string? BannerLink { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey(nameof(BannerCategory))]
        public int BannerCategoryId { get; set; }
        
        [ValidateNever]
        public BannerCategory BannerCategory { get; set; }

    }
}
