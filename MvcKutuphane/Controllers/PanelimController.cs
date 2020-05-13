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

            uyeDb.Ad = uye.Ad;
            uyeDb.Soyad = uye.Soyad;
            uyeDb.Sifre = uye.Sifre;
            uyeDb.KullaniciAdi = uye.KullaniciAdi;
            uyeDb.Okul = uye.Okul;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}