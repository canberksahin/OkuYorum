using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    public class GirisController : BaseController
    {
        // GET: Giris
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kayit(Uyeler uye)
        {
            if (db.Uyeler.FirstOrDefault(x => x.Mail == uye.Mail) == null)
            {
                if (ModelState.IsValid)
                {
                    db.Uyeler.Add(uye);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Kaydınız başarıyla oluşturulmuştur.";
                    return RedirectToAction("Index");
                }
            }
            TempData["ErrorMessage"] = "Bu e-mail adresi veya kullanıcı adıyla mevcut bir kayıt bulunmaktadır.";
                    return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GirisYap(Uyeler uye)
        {
            var bilgiler = db.Uyeler.FirstOrDefault(x => x.Mail == uye.Mail && x.Sifre == uye.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Mail, false);
                Session["Mail"] = bilgiler.Mail.ToString();
                Session["KullaniciAdi"] = bilgiler.KullaniciAdi.ToString();
                return RedirectToAction("Index", "Panelim");
            }
            else
            {
                return View();
            }
        }
        public ActionResult CikisYap()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}