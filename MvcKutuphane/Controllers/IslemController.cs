using MvcKutuphane.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    [AdminAttribute]
    public class IslemController : BaseController
    {
        // GET: Islem
        public ActionResult Index()
        {
            return View(db.Hareket.Where(x=>x.IslemDurum==true).OrderByDescending(x=>x.Id).ToList());
        }
    }
}