using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proje_a.Models.EntityFramework;
using proje_a.Controllers;
using System.Data.Entity.Core.Metadata.Edm;
using proje_a.ViewModel;

namespace proje_a.Controllers
{
    public class HomeController : Controller
    {
        siteDbEntities6 db = new siteDbEntities6();
		// GET: Home
		[HttpGet]
		public ActionResult Index()
		{
			CityViewModel cityview = new CityViewModel();
			cityview.citylist = db.City.ToList();
			//var cities = new List<City>();
			
			//cities.Add(new City() { Title = cityview.citylist., Lat = 40.766279, Lng = 29.937569 });
			//cities.Add(new City() { Title = "MUTLULUK YERİ Cedit, Çetin Sk. No:10, 41300 İzmit/Kocaeli", Lat = 40.769078, Lng = 29.939313 });		
			//cities.Add(new City() { Title = "MUTLULUK YERİ Bahçelievler, Salih Omurtak Sk., 55070 İlkadım/Samsun", Lat = 41.278779, Lng = 36.338922 });			
			//cities.Add(new City() { Title = "MUTLULUK YERİ Tophane, Menzil Sk. No:12, 53020 Rize Merkez/Rize", Lat = 41.022522, Lng = 40.523741 });
			//cities.Add(new City() { Title = "MUTLULUK YERİ Atatürk, Ali Kemali Cd No:5, 24180 Erzincan Merkez/Erzincan", Lat = 39.747273, Lng = 39.495342 });			 
			//cities.Add(new City() { Title = "MUTLULUK YERİ Limonluk, 2436. Sk. 5, 33120 Yenişehir/Mersin", Lat = 36.802440, Lng = 34.590219 });			 
			//cities.Add(new City() { Title = "MUTLULUK YERİ 216. Sk. 46 İsmetpaşa, 35680 Foça / İzmir", Lat = 38.670404, Lng = 26.756068 });
			//cities.Add(new City() { Title = "MUTLULUK YERİ 796. Sk. 6 Karşıyaka, 06830 Gölbaşı / Ankara", Lat = 39.802467, Lng = 32.798396 });
			//cities.Add(new City() { Title = "MUTLULUK YERİ 2929.Sk.Yedişehitler,32200 Isparta Merkez / Isparta", Lat = 37.773531, Lng = 30.544382 });

			return View(cityview);
		}

	
		public ActionResult YerEkle()
		{
			
			return View();
		}
		public ActionResult YerEkleme(string Title, double Lat, double Lng)
		{
			City city = new City();
			city.Title = Title;
			city.Lat = Lat;
			city.Lng = Lng;
			db.City.Add(city);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

	
	


	
	
	
	
	
	
	}
}
