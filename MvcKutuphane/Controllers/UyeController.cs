using MvcKutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class UyeController : BaseController
    {
        // GET: Uyeler
        public ActionResult Index(int sayfa =1)
        {
            var degerler = db.Uyeler.ToList().ToPagedList(sayfa, 10);
            return View(degerler);
        }

        public ActionResult Yeni()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Yeni(Uyeler uye)
        {
            if (ModelState.IsValid)
            {
                db.Uyeler.Add(uye);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            var uye = db.Uyeler.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            db.Uyeler.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Duzenle(int id)
        {
            var uye = db.Uyeler.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(Uyeler uye)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uye).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}