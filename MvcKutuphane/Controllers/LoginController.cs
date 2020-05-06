using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult GirisYap(Uyeler uye)
        {
            var bilgiler = db.Uyeler.FirstOrDefault(x => x.Mail == uye.Mail && x.Sifre == uye.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Mail, false);
                return RedirectToAction("Index", "Panelim");
            }
            else
            {
                return View();
            }
        }
    }
}