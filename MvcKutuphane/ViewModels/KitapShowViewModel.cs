using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcKutuphane.ViewModels
{
    public class KitapShowViewModel
    {
        public Kitap Kitap { get; set; }

        public YorumViewModel YorumViewModel { get; set; }
    }
}