using MvcKutuphane.Authorization;
using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    [AdminAttribute]
    public class IstatistikController : BaseController
    {
        // GET: Istatistik
        public ActionResult Index()
        {
            var todos = (List<string>)Session["todos"];
            var deger1 = db.Uyeler.Count();
            ViewBag.dgr1 = deger1;
            var deger2 = db.Kitap.Count();
            ViewBag.dgr2 = deger2;
            var deger3 = db.Kitap.Where(x=>x.Durum==false).Count();
            ViewBag.dgr3 = deger3;
            var deger4 = db.Cezalar.Sum(x=>x.Para);
            ViewBag.dgr4 = deger4;
            return View(todos);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(string todoItem)
        {
            var todos = (List<string>)Session["todos"];

            if (!string.IsNullOrWhiteSpace(todoItem))
            {
                todos.Add(todoItem.Trim());
            }

            return RedirectToAction("Index");
        }

        public ActionResult OturumuSil()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult Hava()
        {
            return View();
        }

        public ActionResult HavaKart()
        {
            return View();
        }

        public ActionResult Galeri()
        {
            return View(db.Kitap.ToList());
        }

        public ActionResult Cartlar()
        {
            var deger1 = db.Kitap.Count();
            ViewBag.dgr1 = deger1;

            var deger2 = db.Uyeler.Count();
            ViewBag.dgr2 = deger2;

            var deger3 = db.Cezalar.Sum(x => x.Para);
            ViewBag.dgr3 = deger3;

            var deger4 = db.Kitap.Where(x => x.Durum == false).Count();
            ViewBag.dgr4 = deger4;

            var deger5 = db.Kategori.Count();
            ViewBag.dgr5 = deger5;

            var deger6 = db.EnAktifUye().FirstOrDefault();
            ViewBag.dgr6 = deger6;

            var deger7 = db.EnAktifKitap().FirstOrDefault();
            ViewBag.dgr7 = deger7;

            var deger8 = db.EnFazlaKitapYazar().FirstOrDefault();
            ViewBag.dgr8 = deger8;

            var deger9 = db.EnIyiYayinEvi().FirstOrDefault();
            ViewBag.dgr9 = deger9;

            var deger10 = db.EnIyiPersonel().FirstOrDefault();
            ViewBag.dgr10 = deger10;

            var deger11 = 0;
            ViewBag.dgr11 = deger11;

            var deger12 = db.AlinanKitapSayisi().FirstOrDefault();
            ViewBag.dgr12 = deger12;
            return View();
        }
    }
}