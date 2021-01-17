using proje_a.Models.EntityFramework;
using proje_a.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proje_a.Controllers;
using proje_a.Models.Component;
using System.Data.Entity;




namespace proje_a.Controllers
{
    public class BlogController : Controller
    {
        siteDbEntities6 db = new siteDbEntities6();
        // GET: Blog
        public ActionResult Index()
        {
            BlogViewModel post = new BlogViewModel();
            post.articleList = db.blogTbl.ToList();
            return View(post);

        }
        public ActionResult ArticleAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ArticleAdd(blogTbl model)
        {
            blogTbl ekle = new blogTbl();
            ekle.article_title = model.article_title;
            ekle.article = model.article;

            db.blogTbl.Add(ekle);
            db.SaveChanges();
            ViewBag.sonuc = "Makale Kaydı Yapıldı";
            return RedirectToAction("Index");
        }

        public ActionResult icerikDetay(int id)
        {
            siteDbEntities6 db = new siteDbEntities6();
            BlogViewModel makale = new BlogViewModel();

            makale.icerikDetay = db.blogTbl.Where(x => x.article_id == id).ToList();
            return View(makale);

        }

        public ActionResult icerikSil(int id)
        {
            var silinecekIcerik = db.blogTbl.Where(x => x.article_id == id).FirstOrDefault();
            db.blogTbl.Remove(silinecekIcerik);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }

}