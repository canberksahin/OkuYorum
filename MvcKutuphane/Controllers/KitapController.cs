using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class KitapController : BaseController
    {
        public ActionResult Index(string p)
        {
            var kitaplar = from k in db.Kitap select k;
            if (!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(x => x.Ad.Contains(p));
            }
            return View(kitaplar.ToList());
        }

        public ActionResult Yeni()
        {
            ViewBag.Kategori = new SelectList(db.Kategori.ToList(), "Id", "Ad");
            ViewBag.Yazar = new SelectList(db.Yazar.ToList(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Yeni(Kitap kit)
        {
            if (ModelState.IsValid)
            {
                db.Kitap.Add(kit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Kategori = new SelectList(db.Kategori.ToList(), "Id", "Ad");
            ViewBag.Yazar = new SelectList(db.Yazar.ToList(), "Id", "FullName");
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            var kat = db.Kitap.Find(id);
            if (kat == null)
            {
                return HttpNotFound();
            }
            db.Kitap.Remove(kat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Duzenle(int id)
        {
            var yaz = db.Kitap.Find(id);
            if (yaz == null)
            {
                return HttpNotFound();
            }
            ViewBag.Kategori = new SelectList(db.Kategori.ToList(), "Id", "Ad");
            ViewBag.Yazar = new SelectList(db.Yazar.ToList(), "Id", "FullName");
            return View(yaz);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(Kitap yaz)
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