using proje_a.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proje_a.Controllers;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using proje_a.ViewModel;
using proje_a.Models.Component;
using System.Data.Entity;

namespace proje_a.Controllers
{
    public class DestekController : Controller
    {
        siteDbEntities6 db = new siteDbEntities6();
        // GET: Destek
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult Bagis()
        {
            return View();
        }
    }
}