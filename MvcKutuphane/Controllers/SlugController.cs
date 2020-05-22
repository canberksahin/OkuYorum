using MvcKutuphane.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class SlugController : Controller
    {
        [HttpPost]
        public string ConvertToSlug(string title)
        {
            return UrlService.URLFriendly(title);
        }
    }
}