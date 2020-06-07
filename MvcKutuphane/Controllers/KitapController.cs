using MvcKutuphane.Authorization;
using MvcKutuphane.Common;
using MvcKutuphane.Models.Entity;
using MvcKutuphane.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    [AdminAttribute]
    public class KitapController : BaseController
    {
        [HttpPost]
        public ActionResult UploadProfile(string photoBase64)
        {
            if (string.IsNullOrEmpty(photoBase64))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string fileName = this.SaveProfilePhoto(photoBase64);
            string mail = Session["Mail"].ToString();
            var user = db.Uyeler.FirstOrDefault(x=>x.Mail==mail);
            this.UrunResimSil(user.Fotograf, "Profiles");
            user.Fotograf = fileName;
            db.SaveChanges();

            return Json(new { photoUrl = Url.ProfilePhoto(fileName) });
        }

        [HttpPost]
        public ActionResult DeleteProfile()
        {
            string mail = Session["Mail"].ToString();
            var user = db.Uyeler.FirstOrDefault(x => x.Mail == mail);
            this.UrunResimSil(user.Fotograf, "Profiles");
            user.Fotograf = null;
            db.SaveChanges();

            return Json(new { photoUrl = Url.ProfilePhoto(null) });
        }

        public ActionResult Index(string p)
        {
            var kitaplar = from k in db.Kitap select k;
            kitaplar = kitaplar.Where(x => x.Durum == true);
            if (!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(x => x.Ad.Contains(p));
            }
            kitaplar = kitaplar.Where(x => x.Kategori1.Durum == true);
            return View(kitaplar.ToList());
        }

        public ActionResult Yeni()
        {
            ViewBag.Kategori = new SelectList(db.Kategori.ToList(), "Id", "Ad");
            ViewBag.Yazar = new SelectList(db.Yazar.ToList(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Yeni(Kitap kit)
        {
            if (ModelState.IsValid)
            {
                kit.KitapKapak = this.UrunResimKaydet(kit.Foto);
                db.Kitap.Add(kit);
                kit.Slug = UrlService.URLFriendly(kit.Slug);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Yeni kitap başarıyla eklenmiştir.";
                return RedirectToAction("Index");
            }
            ViewBag.Kategori = new SelectList(db.Kategori.ToList(), "Id", "Ad");
            ViewBag.Yazar = new SelectList(db.Yazar.ToList(), "Id", "FullName");
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            var kat = db.Kitap.Find(id);
            if (kat == null)
            {
                return HttpNotFound();
            }
            db.Kitap.Remove(kat);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Kitap başarıyla silinmiştir.";
            return RedirectToAction("Index");
        }

        public ActionResult Duzenle(int id)
        {
            var yaz = db.Kitap.Find(id);
            if (yaz == null)
            {
                return HttpNotFound();
            }
            ViewBag.Kategori = new SelectList(db.Kategori.ToList(), "Id", "Ad");
            ViewBag.Yazar = new SelectList(db.Yazar.ToList(), "Id", "FullName");
            return View(yaz);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(Kitap yaz)
        {
            var dbKitap = db.Kitap.Find(yaz.Id);
            if (ModelState.IsValid)
            {
                #region Kitap Foto Güncelle
                if (yaz.Foto != null)
                {
                    this.UrunResimSil(dbKitap.KitapKapak); // eskiyi sil
                    dbKitap.KitapKapak = this.UrunResimKaydet(yaz.Foto); // yeniyi kaydet
                }
                #endregion
                dbKitap.Ad = yaz.Ad;
                dbKitap.BasımYıl = yaz.BasımYıl;
                dbKitap.Kategori = yaz.Kategori;
                dbKitap.Sayfa = yaz.Sayfa;
                dbKitap.Slug = UrlService.URLFriendly(yaz.Slug);
                dbKitap.Yazar = yaz.Yazar;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Kitap başarıyla düzenlenmiştir.";
                return RedirectToAction("Index");
            }
            ViewBag.Kategori = new SelectList(db.Kategori.ToList(), "Id", "Ad");
            ViewBag.Yazar = new SelectList(db.Yazar.ToList(), "Id", "FullName");
            return View(yaz);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UrunResimSil(int id)
        {
            var urun = db.Kitap.Find(id);

            if (urun == null)
            {
                return HttpNotFound();
            }

            this.UrunResimSil(urun.KitapKapak);

            urun.KitapKapak = "";
            db.SaveChanges();

            return RedirectToAction("Duzenle", new { id = id });
        }

        // GET: Kitaplar/333/sample-post-1
        [Route("Kitaplar/{id}/{slug?}")]
        public ActionResult Show(int id, string slug)
        {
            var kitap = db.Kitap.Find(id);
            if (kitap == null)
            {
                return HttpNotFound();
            }
            if (kitap.Slug != slug)
            {
                return RedirectToAction("Show", new { id = id, slug = kitap.Slug });
            }
            var vm = new MvcKutuphane.ViewModels.KitapShowViewModel
            {
                Kitap = kitap,
                YorumViewModel = new ViewModels.YorumViewModel()
            };

            return View(vm);
        }

        [Route("Kitaplar/{id}/{slug?}")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Show(int id, string slug, YorumViewModel commentViewModel)
        {
            var post = db.Kitap.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (post.Slug != slug)
            {
                return RedirectToAction("Show", new { id = id, slug = post.Slug });
            }

            if (ModelState.IsValid)
            {
                string mail = Session["mail"].ToString();
                var yazar = db.Uyeler.FirstOrDefault(z=>z.Mail==mail);
                var comment = new Yorums
                {
                    YazarId = yazar.Id,
                    Icerik = commentViewModel.Content,
                    YayinlanmaZamani = DateTime.Now,
                    DegistirmeZamani = DateTime.Now,
                    Durum = true,
                    KitapId = id,
                    ParentId = commentViewModel.ParentId
                };
                db.Yorums.Add(comment);
                db.SaveChanges();
                return Redirect(Url.RouteUrl(new { controller = "Kitap", action = "Show", id = id, slug = slug, commentSuccess = true }) + "#leave-a-comment");

            }

            var vm = new KitapShowViewModel
            {
                Kitap = post,
                YorumViewModel = commentViewModel
            };
            return View(vm);
        }

        public ActionResult CategoriesPartial()
        {
            return PartialView("_CategoriesPartial", db.Kategori.OrderBy(X => X.Ad).ToList());
        }
    }
}