using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace MvcKutuphane.Common
{
    public class UrunResmiAttribute : ValidationAttribute
    {
        public string IzinVerilenUzantilar { get; set; } = ".jpg, .jpeg, .png";

        public int MaxFileSizeMB { get; set; } = 1;

        public override bool IsValid(object value)
        {
            if (value == null || !(value is HttpPostedFileBase))
                return true;

            string[] izinliUzantilar = IzinVerilenUzantilar.ToLower(CultureInfo.InvariantCulture)
                .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var resim = (HttpPostedFileBase)value;

            var ext = Path.GetExtension(resim.FileName);

            if (!resim.ContentType.StartsWith("image/") || !izinliUzantilar.Contains(ext.ToLower(CultureInfo.InvariantCulture)))
            {
                ErrorMessage = "Kabul edilen dosya türleri: " + IzinVerilenUzantilar;
                return false;
            }
            else if (resim.ContentLength > MaxFileSizeMB * 1024 * 1024)
            {
                ErrorMessage = $"Resim dosyası {MaxFileSizeMB}MB'dan büyük olamaz.";
                return false;
            }

            return true;
        }
    }
}