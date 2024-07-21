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
    public class Content
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ContentAvata { get; set; }
        public string? ShortDes { get; set; }
        public string? Author { get; set; }
        public string? ContentSource { get; set; }
        public DateOnly? Created_Date { get; set; }
        public DateOnly? Updated_Date { get; set; }
        public bool IsAcive { get; set; }
        public bool IsSpecial { get; set; }
        public bool Approved { get; set; }
        [ForeignKey(nameof(ContentCategory))]
        public int ContentCategoryId { get; set; }
        [ValidateNever]
        public ContentCategory ContentCategory { get; set; }
    }
}
