using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcKutuphane.Common
{
    public static class UrunIslemleri
    {
        public static void UrunResimSil(this Controller controller, string resimYolu)
        {
            if (!string.IsNullOrEmpty(resimYolu))
            {
                var resimTamYolu = Path.Combine(controller.Server.MapPath("~/Upload"), resimYolu);

                if (System.IO.File.Exists(resimTamYolu))
                {
                    System.IO.File.Delete(resimTamYolu);
                }
            }
        }

        public static string UrunResimKaydet(this Controller controller, HttpPostedFileBase resim)
        {
            if (resim == null)
                return "";

            string dizin = controller.Server.MapPath("~/Upload/");
            string dosyaAd = Guid.NewGuid() + Path.GetExtension(resim.FileName);
            string kaydetYol = Path.Combine(dizin, dosyaAd);
            #region Foto Düzenleme
            // https://stackoverflow.com/questions/17079090/how-to-resize-and-save-an-image-which-uploaded-using-file-upload-control-in-c-sh
            WebImage img = new WebImage(resim.InputStream);
            int minBoyut = Math.Min(img.Width, img.Height);
            int maxBoyut = Math.Max(img.Width, img.Height);
            int kirpmaMiktar = (maxBoyut - minBoyut) / 4;

            //if (img.Height > minBoyut)
            //{
            //    img.Crop(kirpmaMiktar, 0, kirpmaMiktar, 0);
            //}
            //else if (img.Width > minBoyut)
            //{
            //    img.Crop(0, kirpmaMiktar, 0, kirpmaMiktar);
            //}
            #endregion

            img.Save(kaydetYol);

            return dosyaAd;
        }

        public static string UrunResim(this UrlHelper urlHelper, string resimYolu)
        {
            if (string.IsNullOrEmpty(resimYolu))
            {
                return urlHelper.Content("~/Images/noimage.png");
            }

            return urlHelper.Content("~/Upload/" + resimYolu);
        }
    }
}