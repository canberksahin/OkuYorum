using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcKutuphane.ViewModels
{
    public class YorumViewModel
    {
        [StringLength(4000), Required]
        public string Content { get; set; }

        public int? ParentId { get; set; }
    }
}