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

        public ActionResult YeniMesaj()
        {
            return View();
        }
    }
}