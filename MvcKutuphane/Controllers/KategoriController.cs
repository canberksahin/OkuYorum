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

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Yeni(Kategori kat)
        {
            if (ModelState.IsValid)
            {
                db.Kategori.Add(kat);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Yeni kategori başarıyla oluşturulmuştur.";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            var kat = db.Kategori.Find(id);
            if (kat == null)
            {
                return HttpNotFound();
            }
            if (kat.Kitap.Count > 0)
            {
                TempData["ErrorMessage"] = "Bu kategorinin altında yüklü kitaplar olduğu için silinemez.";
                return RedirectToAction("Index");
            }
            db.Kategori.Remove(kat);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Kategori başarıyla silinmiştir.";
            return RedirectToAction("Index");
        }

        public ActionResult Duzenle(int id)
        {
            var kat = db.Kategori.Find(id);
            if (kat == null)
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
                TempData["SuccessMessage"] = "Kategori başarıyla düzenlenmiştir.";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DurumDegistir(int id, bool isPublished)
        {
            var comment = db.Kategori.Find(id);

            comment.Durum = isPublished ? true : false;
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        // GET: Kategoriler/333/sample-kategori-1
        [Route("Kategoriler/{katid}/{slugg?}/{ara?}")]
        public ActionResult Show(int katid, string slugg, string ara = "")
        {
            var kat = db.Kategori.Find(katid);
            if (kat == null)
            {
                return HttpNotFound();
            }
            if (kat.Slug != slugg)
            {
                return RedirectToAction("Show", new { katid = katid, slugg = kat.Slug });
            }
            if (ara != "")
            {
                var kitaplar = from k in db.Kitap select k;
                kitaplar = kitaplar.Where(x => x.Kategori == katid);
                kitaplar = kitaplar.Where(x => x.Ad.Contains(ara));
                return View(kitaplar.ToList());
            }
            else
            {
                ICollection<Kitap> kitaplar = kat.Kitap;
                return View(kitaplar);
            }

        }
    }
}