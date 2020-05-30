using MvcKutuphane.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            VitrinIndexViewModel vm = new VitrinIndexViewModel();
            vm.Kitaplar = db.Kitap.ToList();
            vm.Hakkimizda = db.Hakkimizda.ToList();
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}