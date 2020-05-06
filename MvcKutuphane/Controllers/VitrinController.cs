using MvcKutuphane.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class VitrinController : BaseController
    {
        // GET: Vitrin
        public ActionResult Index()
        {
            VitrinIndexViewModel vm = new VitrinIndexViewModel();
            vm.Kitaplar = db.Kitap.ToList();
            vm.Hakkimizda = db.Hakkimizda.ToList();
            return View(vm) ;
        }
    }
}