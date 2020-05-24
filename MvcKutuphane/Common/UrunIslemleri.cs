using MvcKutuphane.Models.Entity;
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
        public static void UrunResimSil(this Controller controller, string resimYolu, string folderName="")
        {
            if (!string.IsNullOrEmpty(resimYolu))
            {
                var resimTamYolu = Path.Combine(controller.Server.MapPath("~/Upload/" + folderName), resimYolu);

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

        public static string SaveProfilePhoto(this Controller controller, string imgBase64)
        {
            if (string.IsNullOrEmpty(imgBase64))
                return null;

            byte[] data = Convert.FromBase64String(imgBase64.Substring(22));
            string fileName = Guid.NewGuid() + ".png";
            string savePath = Path.Combine(
                controller.Server.MapPath("~/Upload/Profiles"), fileName
                );
            System.IO.File.WriteAllBytes(savePath, data);

            return fileName;
        }

        public static string ProfilePhoto(this UrlHelper urlHelper, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return urlHelper.Content("~/Images/no-user.png");
            }

            return urlHelper.Content("~/Upload/Profiles/" + fileName);
        }

        public static string LoggedInProfilePhoto(this UrlHelper urlHelper)
        {
            string mail = HttpContext.Current.Session["Mail"].ToString();
            DbKutuphaneEntities db = new DbKutuphaneEntities();
            var uye = db.Uyeler.FirstOrDefault(x => x.Mail == mail);
            string fileName = uye.Fotograf;

            return urlHelper.ProfilePhoto(fileName);
        }
    }
}