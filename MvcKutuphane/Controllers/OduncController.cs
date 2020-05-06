using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class OduncController : BaseController
    {
        // GET: Odunc
        public ActionResult Index()
        {
            return View(db.Hareket.Where(x=>x.IslemDurum==false).ToList());
        }

        public ActionResult OduncVer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OduncVer(Hareket har)
        {
            if (ModelState.IsValid)
            {
                db.Hareket.Add(har);
                db.SaveChanges();

                return RedirectToAction("Index","Kitap");
            }
            return View();
        }


        public ActionResult OduncIade(Hareket h)
        {
            var od = db.Hareket.Find(h.Id);

            DateTime d1 = DateTime.Parse(od.IadeTarihi.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortTimeString());
            TimeSpan d3 = d2 - d1;
            double a = d3.TotalDays;
            if (a<0)
            {
                a = 0;
            }
            int b = (int)a;
            ViewBag.dgr = b;
            return View("OduncIade",od);
        }

        public ActionResult OduncGuncelle(Hareket h)
        {
            var har = db.Hareket.Find(h.Id);
            if (h == null)
            {
                return HttpNotFound();
            }
            har.UyeGetirTarih = h.UyeGetirTarih;
            har.IslemDurum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}