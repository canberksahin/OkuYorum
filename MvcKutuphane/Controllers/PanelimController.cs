using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class PanelimController : BaseController
    {
        // GET: Panelim
        [Authorize]
        public ActionResult Index()
        {
            var uyeMail = (string)Session["Mail"];
            var degerler = db.Uyeler.FirstOrDefault(x => x.Mail == uyeMail);
            return View(degerler);
        }
        public ActionResult ProfilGuncelle()
        {
            var uyeMail = (string)Session["Mail"];
            var degerler = db.Uyeler.FirstOrDefault(x => x.Mail == uyeMail);
            return View(degerler);
        }

        [HttpPost]
        public ActionResult ProfilGuncelle(Uyeler uye)
        {
            var uyeMail = (string)Session["Mail"];
            var uyeDb = db.Uyeler.FirstOrDefault(x => x.Mail == uyeMail);
            Session["KullaniciAdi"] = uye.KullaniciAdi;
            uyeDb.Ad = uye.Ad;
            uyeDb.Soyad = uye.Soyad;
            uyeDb.Sifre = uye.Sifre;
            uyeDb.KullaniciAdi = uye.KullaniciAdi;
            uyeDb.Okul = uye.Okul;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Kitaplarım()
        {
            var uyeMail = (string)Session["Mail"];
            var uyeDb = db.Uyeler.FirstOrDefault(x => x.Mail == uyeMail);
            return View(db.Yorums.Where(x => x.YazarId == uyeDb.Id).OrderByDescending(x => x.YayinlanmaZamani).ToList());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            var kat = db.Yorums.Find(id);
            if (kat == null)
            {
                return HttpNotFound();
            }
            db.Yorums.Remove(kat);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Yorum başarıyla silinmiştir.";
            return RedirectToAction("Kitaplarım","Panelim");
        }
    }
}