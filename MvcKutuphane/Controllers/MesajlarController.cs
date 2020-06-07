using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class MesajlarController : BaseController
    {
        // GET: Mesajlar
        public ActionResult Index()
        {
            var uyeMail = (string)Session["Mail"].ToString();
            return View(db.Mesajlar.Where(x=>x.Alici==uyeMail).ToList());
        }

        public ActionResult GidenKutusu()
        {
            var uyeMail = (string)Session["Mail"].ToString();
            return View(db.Mesajlar.Where(x => x.Gonderen == uyeMail).ToList());
        }

        public ActionResult YeniMesaj()
        {
            return View();
        }

        public ActionResult Cevapla(int id)
        {
            var yazar = db.Uyeler.FirstOrDefault(x=>x.Id==id);
            var mail = yazar.Mail;
            var mesaj = new Mesajlar
            {
                Alici = mail
            };
            return View(mesaj);
        }


        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            if (ModelState.IsValid)
            {
                var uyeMail = (string)Session["Mail"].ToString();
                m.Gonderen = uyeMail;
                m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
                db.Mesajlar.Add(m);
                db.SaveChanges();

                return RedirectToAction("GidenKutusu","Mesajlar");
            }
            return View();
        }
    }
}