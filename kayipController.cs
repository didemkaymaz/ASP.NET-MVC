using proje_a.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proje_a.Controllers;
using System.IO;
using proje_a.ViewModel;
using proje_a.Models.Component;
using System.Data.Entity;

namespace proje_a.Controllers
{
    public class kayipController : Controller
    {
        siteDbEntities6 db = new siteDbEntities6();

        // GET: kayip
        public ActionResult Index(String searchString)
        {
            
            kayipTblViewModel kayiplar = new kayipTblViewModel();
            using (siteDbEntities6 db = new siteDbEntities6())
            {


                kayiplar.kayipList = (from a in db.imageTbl
                                      join b in db.kayipTbl on a.kayip_id equals b.kayip_id
                                      join c in db.uyeTbl on b.uye_id equals c.uye_id

                                      select new kayipListe
                                      {
                                          kayip_id = b.kayip_id,
                                          kayip_ad = b.kayip_ad,
                                          kayip_aciklama = b.kayip_aciklama,
                                          kayip_cinsiyet = b.kayip_cinsiyet,
                                          kayip_il = b.kayip_il,
                                          kayip_renk = b.kayip_renk,
                                          kayip_turu = b.kayip_turu,
                                          kayip_tarihi = b.kayip_tarihi,
                                          kayip_yas = b.kayip_yas,
                                          id_image = a.id_image,
                                          image_name = a.image_name,
                                          uye_id = c.uye_id,
                                          kullanici_adi = c.kullanici_adi,
                                          resim = b.resim
                                      }).ToList();
               

            }

            return View(kayiplar);
            // return View();
        }

            public ActionResult lost()
        {
            kayipTbl kyp = new kayipTbl();
            return View(kyp);
        }
        [HttpPost]

        public ActionResult UploadFiles(HttpPostedFileBase resim, kayipTbl kyp)
        {
            if (ModelState.IsValid)
            {
                int ID_USERS = Convert.ToInt32(Session["uye_id"]);
                var check = db.kayipTbl.FirstOrDefault(s => s.kayip_id == kyp.kayip_id);
                if (check == null)
                {
                    kyp.uye_id = ID_USERS;
                    //kyp.kayip_tarihi = DateTime.Now;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.kayipTbl.Add(kyp);
                    db.SaveChanges();
                    ViewBag.sonuc = "Kayıp Eklendi";
                }

                try
                {

                    if (resim != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(resim.FileName);
                        string extension = Path.GetExtension(resim.FileName);
                        fileName = fileName + extension;
                        imageTbl image = new imageTbl();
                        int idpost = db.kayipTbl.Max(x => x.kayip_id);
                        image.kayip_id = idpost;
                        image.image_name = fileName;
                        db.imageTbl.Add(image);
                        db.SaveChanges();
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(resim.FileName));
                        resim.SaveAs(path);

                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                    ModelState.Clear();
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                }


            }

            return RedirectToAction("lost", "kayip");
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            imageTbl image = new imageTbl();
            using (siteDbEntities6 db = new siteDbEntities6())
            {
                image = db.imageTbl.Where(x => x.id_image == id).FirstOrDefault();
            }

            return View(image);
        }

        public ActionResult KayipDetay(int id)
        {
            kayipTblViewModel kayiplar = new kayipTblViewModel();
            using (siteDbEntities6 db = new siteDbEntities6())
            {
                kayiplar.kayipDetay = (from a in db.imageTbl
                                       join b in db.kayipTbl on a.kayip_id equals b.kayip_id
                                       join c in db.uyeTbl on b.uye_id equals c.uye_id
                                       where b.kayip_id == id
                                       select new kayipDetay
                                       {
                                           kayip_id = b.kayip_id,
                                           kayip_ad = b.kayip_ad,
                                           kayip_aciklama = b.kayip_aciklama,
                                           kayip_cinsiyet = b.kayip_cinsiyet,
                                           kayip_il = b.kayip_il,
                                           kayip_renk = b.kayip_renk,
                                           kayip_turu = b.kayip_turu,
                                           kayip_tarihi = b.kayip_tarihi,
                                           kayip_yas = b.kayip_yas,
                                           id_image = a.id_image,
                                           image_name = a.image_name,
                                           uye_id = c.uye_id,
                                           kullanici_adi = c.kullanici_adi
                                       })
                                       .ToList();
            }

            return View(kayiplar);
        }
        public ActionResult KayipDüzenle(int id)
        {
            
            kayipTblViewModel kayipTblViewModel = new kayipTblViewModel();
            kayipTblViewModel.kayitDüzenle = (from a in db.kayipTbl
                                              join b in db.imageTbl on a.kayip_id equals b.kayip_id
                                              join c in db.uyeTbl on a.uye_id equals c.uye_id
                                              where a.kayip_id == id
                                              select new DüzenlenecekKayit
                                              {
                                                  kayip_id = a.kayip_id,
                                                  kayip_ad = a.kayip_ad,
                                                  kayip_aciklama = a.kayip_aciklama,
                                                  kayip_cinsiyet = a.kayip_cinsiyet,
                                                  kayip_il = a.kayip_il,
                                                  kayip_renk = a.kayip_renk,
                                                  kayip_turu = a.kayip_turu,
                                                  kayip_tarihi = a.kayip_tarihi,
                                                  kayip_yas = a.kayip_yas,
                                                  id_image = b.id_image,
                                                  image_name = b.image_name,
                                                  uye_id = c.uye_id,
                                                  kullanici_adi = c.kullanici_adi
                                              }).First();
            return View(kayipTblViewModel);


        }
        public ActionResult DüzenlemeKaydet(kayipTblViewModel kayitGüncelle)
        {
            var güncelleme = db.kayipTbl.SingleOrDefault(x => x.kayip_id == kayitGüncelle.kayitDüzenle.kayip_id);
            güncelleme.kayip_aciklama = kayitGüncelle.kayitDüzenle.kayip_aciklama;
            güncelleme.kayip_ad = kayitGüncelle.kayitDüzenle.kayip_ad;
            güncelleme.kayip_cinsiyet = kayitGüncelle.kayitDüzenle.kayip_cinsiyet;
            güncelleme.kayip_il = kayitGüncelle.kayitDüzenle.kayip_il;
            güncelleme.kayip_renk = kayitGüncelle.kayitDüzenle.kayip_renk;
            güncelleme.kayip_tarihi = kayitGüncelle.kayitDüzenle.kayip_tarihi;
            güncelleme.kayip_turu = kayitGüncelle.kayitDüzenle.kayip_turu;
            güncelleme.kayip_yas = kayitGüncelle.kayitDüzenle.kayip_yas;
            db.Entry(güncelleme).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KayipSil(int id)
        {
            var silinecekKayip = db.kayipTbl.Where(x => x.kayip_id == id).FirstOrDefault();
            db.kayipTbl.Remove(silinecekKayip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Arama(string name)
        {
            kayipTblViewModel postViewModel = new kayipTblViewModel();
            if (!String.IsNullOrEmpty(name))
            {
                postViewModel.aramaList = db.kayipTbl.Where(p => p.kayip_turu.Contains(name) || p.kayip_il.Contains(name)).ToList();
            }
            return View(postViewModel);
        }










    }
}