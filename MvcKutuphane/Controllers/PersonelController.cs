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
    public class PersonelController : BaseController
    {
        // GET: Personel
        public ActionResult Index()
        {
            return View(db.Personel.ToList());
        }

        public ActionResult Yeni()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Yeni(Personel per)
        {
            if (ModelState.IsValid)
            {
                db.Personel.Add(per);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            var per = db.Personel.Find(id);
            if (per == null)
            {
                return HttpNotFound();
            }
            db.Personel.Remove(per);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Duzenle(int id)
        {
            var per = db.Personel.Find(id);
            if (per == null)
            {
                return HttpNotFound();
            }
            return View(per);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(Personel per)
        {
            if (ModelState.IsValid)
            {
                db.Entry(per).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}