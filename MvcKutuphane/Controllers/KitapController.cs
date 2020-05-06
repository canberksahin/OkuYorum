using MvcKutuphane.Common;
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
            kitaplar = kitaplar.Where(x => x.Durum == true);
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
                kit.KitapKapak = this.UrunResimKaydet(kit.Foto);
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
            var dbKitap = db.Kitap.Find(yaz.Id);
            if (ModelState.IsValid)
            {
                #region Kitap Foto Güncelle
                if (yaz.Foto != null)
                {
                    this.UrunResimSil(dbKitap.KitapKapak); // eskiyi sil
                    dbKitap.KitapKapak = this.UrunResimKaydet(yaz.Foto); // yeniyi kaydet
                }
                #endregion
                dbKitap.Ad = yaz.Ad;
                dbKitap.BasımYıl = yaz.BasımYıl;
                dbKitap.Kategori = yaz.Kategori;
                dbKitap.Sayfa = yaz.Sayfa;
                dbKitap.Yazar = yaz.Yazar;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Kategori = new SelectList(db.Kategori.ToList(), "Id", "Ad");
            ViewBag.Yazar = new SelectList(db.Yazar.ToList(), "Id", "FullName");
            return View(yaz);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UrunResimSil(int id)
        {
            var urun = db.Kitap.Find(id);

            if (urun == null)
            {
                return HttpNotFound();
            }

            this.UrunResimSil(urun.KitapKapak);

            urun.KitapKapak = "";
            db.SaveChanges();

            return RedirectToAction("Duzenle", new { id = id });
        }
    }
}