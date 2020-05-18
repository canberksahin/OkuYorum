using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcKutuphane.Enumlar
{
    public enum Durum
    {
        [Display(Name = "Pending Review")]
        Beklemede,
        [Display(Name = "Published")]
        Onaylandı,
        [Display(Name = "Not Published")]
        Reddedildi
    }
}
