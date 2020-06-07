using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Authorization
{
    public class AdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["Mail"].ToString() != "canberksahin20@gmail.com")
            {
                return false;
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}