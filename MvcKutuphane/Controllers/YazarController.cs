using MvcKutuphane.Authorization;
using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    [AdminAttribute]
    public class YazarController : BaseController
    {
        // GET: Yazar
        public ActionResult Index()
        {
            return View(db.Yazar.ToList());
        }

        public ActionResult Yeni()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Yeni(Yazar yaz)
        {
            if (ModelState.IsValid)
            {
                db.Yazar.Add(yaz);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            var kat = db.Yazar.Find(id);
            if (kat == null)
            {
                return HttpNotFound();
            }
            db.Yazar.Remove(kat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Duzenle(int id)
        {
            var yaz = db.Yazar.Find(id);
            if (yaz == null)
            {
                return HttpNotFound();
            }
            return View(yaz);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(Yazar yaz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yaz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}