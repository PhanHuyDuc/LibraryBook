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
    public class ContentImage
    {
        [Key]
        public int Id { get; set; }             
        public string? MultiImage { get; set; }
        [ForeignKey(nameof(Content))]
        public int ContentId { get; set; }
        [ValidateNever]
        public Content Content { get; set; }
    }
}
