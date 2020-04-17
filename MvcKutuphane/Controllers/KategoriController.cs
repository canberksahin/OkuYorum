using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class KategoriController : BaseController
    {
        
        public ActionResult Index()
        {
            var degerler = db.Kategori.ToList();
            return View(degerler);
            
        }

        public ActionResult Yeni()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Yeni(Kategori kat)
        {
            if (ModelState.IsValid)
            {
                db.Kategori.Add(kat);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost ,ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            var kat=db.Kategori.Find(id);
            if (kat==null)
            {
                return HttpNotFound();
            }
            db.Kategori.Remove(kat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Duzenle(int id)
        {
            var kat = db.Kategori.Find(id);
            if (kat==null)
            {
                return HttpNotFound();
            }
            return View(kat);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(Kategori kat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}