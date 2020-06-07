using MvcKutuphane.Controllers;
using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcKutuphane.Models.Classes
{
    public class VitrinIndexViewModel
    {
        public List<Kitap> Kitaplar { get; set; }
        public IEnumerable<Hakkimizda> Hakkimizda { get; set; }

        public List<Yorums> Yorumlar { get; set; }
    }
}