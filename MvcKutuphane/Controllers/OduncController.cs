using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class OduncController : BaseController
    {
        // GET: Odunc
        public ActionResult Index()
        {
            return View(db.Hareket.ToList());
        }

        public ActionResult OduncVer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OduncVer(Hareket har)
        {
            if (ModelState.IsValid)
            {
                db.Hareket.Add(har);
                db.SaveChanges();

                return RedirectToAction("OduncIade");
            }
            return View();
        }


        public ActionResult OduncIade(int id)
        {
            var od = db.Hareket.Find(id);
            return View("OduncIade",od);
        }
    }
}