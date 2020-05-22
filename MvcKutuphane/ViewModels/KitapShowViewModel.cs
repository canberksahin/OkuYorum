using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcKutuphane.ViewModels
{
    public class KitapShowViewModel
    {
        public List<Kitap> Kitaplar { get; set; }

        public List<Yorums> Yorumlar { get; set; }
    }
}