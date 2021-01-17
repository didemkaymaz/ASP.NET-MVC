using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proje_a.Models;
using proje_a.Models.EntityFramework;
using proje_a.Controllers;
using System.Data.Entity.Core.Metadata.Edm;
using System.Web.Security;
using proje_a.Models.Component;
using System.Data.Entity;
using uyeTbl = proje_a.Models.EntityFramework.uyeTbl;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using System.Net.Mail;
using System.Net;

namespace proje_a.Controllers
{

    public class UyeController : Controller
    {
        siteDbEntities6 db = new siteDbEntities6();
        public ActionResult Index()
        {
            return View();
        }
        // GET: Uye
        public ActionResult uyelik()
        {
            Models.EntityFramework.uyeTbl user = new Models.EntityFramework.uyeTbl();
            return View(user);
        }
        [HttpPost]
        public ActionResult uyelik(Models.EntityFramework.uyeTbl user)
        {
            if (ModelState.IsValid)
            {
                var check = db.uyeTbl.FirstOrDefault(s => s.uye_mail == user.uye_mail);
                if (check == null)
                {

                    user.rol = "üye";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.uyeTbl.Add(user);
                    db.SaveChanges();
                    
                }
                else
                {
                    ViewBag.Mesaj = "Bu email adresi zaten mevcut!!!!";
                    return View();
                }


            }
            return RedirectToAction("login");
        }
        public ActionResult login()
        {
            Models.EntityFramework.uyeTbl usersViewModel = new Models.EntityFramework.uyeTbl();
            return View(usersViewModel);
        }
        [HttpPost]
        public ActionResult Login(Models.EntityFramework.uyeTbl user)
        {
            if (ModelState.IsValid)
            {
                var userInDb = db.uyeTbl.FirstOrDefault(x => x.uye_mail == user.uye_mail && x.uye_sifre == user.uye_sifre);
                if (userInDb != null)
                {
                    Session["uye_id"] = userInDb.uye_id.ToString();
                    Session["uye_ad"] = userInDb.uye_ad.ToString();
                    Session["kullanici_adi"] = userInDb.kullanici_adi.ToString();
                    Session["uye_soyad"] = userInDb.uye_soyad.ToString();
                    Session["uye_sehir"] = userInDb.uye_sehir.ToString();
                    Session["uye_telno"] = userInDb.uye_telno.ToString();
                    Session["uye_mail"] = userInDb.uye_mail.ToString();



                    Session["rol"] = userInDb.rol.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Mesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                    return View();
                }

            }
            return View();
        }
        public ActionResult logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }


        public ActionResult profil()
        {
            if (Session["kullanici_adi"] != null)
            {
                //Response.Write("Hoşgeldin " + Session["kullanici_adi"]);
            }
            return View("profil");
        }

        public ActionResult Guncelle(int id)
        {

            return View("Guncelle", db.uyeTbl.Find(id));
        }

        [HttpPost]
        public ActionResult Guncelle(uyeTbl u)
        {
            int uye_id = Convert.ToInt32(Session["uye_id"]);
            var uyeTbl = db.uyeTbl.Find(u.uye_id);
            uyeTbl.uye_ad = u.uye_ad;
            uyeTbl.uye_soyad = u.uye_soyad;
            uyeTbl.uye_mail = u.uye_mail;
            uyeTbl.uye_telno = u.uye_telno;


            db.Entry(uyeTbl).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("profil", new { id = uye_id });
        }




        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            string resetCode = Guid.NewGuid().ToString();
            var verifyUrl = "/Uye/ResetPassword/" + resetCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            using (var context = new siteDbEntities6())
            {
                var getUser = (from s in context.uyeTbl where s.uye_mail == EmailID select s).FirstOrDefault();
                if (getUser != null)
                {
                    getUser.ResetPasswordCode = resetCode;


                    context.Configuration.ValidateOnSaveEnabled = false;
                    context.SaveChanges();

                    var subject = "Password Reset Request";
                    var body = "Merhaba " + getUser.uye_ad + ", <br/> Kısa süre önce hesabınız için şifrenizi sıfırlamayı istediniz. Sıfırlamak için aşağıdaki bağlantıyı tıklayın. " +

                         " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" +
                         "Parola sıfırlama talebinde bulunmadıysanız, lütfen bu e-postayı dikkate almayın veya bize bildirin.<br/><br/> Teşekkür ederiz.";

                    SendEmail(getUser.uye_mail, body, subject);

                    ViewBag.Message = "Şifre sıfırlama bağlantısı e-posta kimliğinize gönderildi.";
                }
                else
                {
                    ViewBag.Message = "Kullanıcı mevcut değil.";
                    return View();
                }
            }

            return View();
        }

        private void SendEmail(string emailAddress, string body, string subject)
        {
            using (MailMessage mm = new MailMessage("didemkaymaz99@gmail.com", emailAddress))
            {
                mm.Subject = subject;
                mm.Body = body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("didemkaymaz99@gmail.com", "8327107739dk");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

            }
        }

        public ActionResult ResetPassword(string id)
        {
         
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            using (var context = new siteDbEntities6())
            {
                var user = context.uyeTbl.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (var context = new siteDbEntities6())
                {
                    var user = context.uyeTbl.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.uye_sifre = model.NewPassword;
                        user.ResetPasswordCode = "";
                        context.Configuration.ValidateOnSaveEnabled = false;
                        context.SaveChanges();
                        message = "Yeni şifre başarıyla güncellendi";
                    }
                }
            }
            else
            {
                message = "Bir şey geçersiz";
            }
            ViewBag.Message = message;
            return View(model);
        }




        //public ActionResult Edit()
        //{
        //    int id =

        //    // Fetch the userprofile
        //    Models.EntityFramework.uyeTbl user = db.uyeTbl.FirstOrDefault(u => u.kullanici_adi.Equals(username));

        //    // Construct the viewmodel
        //    Models.Component.edit model = new Models.Component.edit();
        //    model.kullanici_adi = user.kullanici_adi;
        //    model.uye_ad = user.uye_ad;
        //    model.uye_soyad = user.uye_soyad;

        //    model.uye_mail = user.uye_mail;
        //    model.uye_telno = user.uye_telno;


        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult Edit(Models.Component.edit userprofile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string username = User.Identity.Name;
        //        // Get the userprofile
        //        Models.EntityFramework.uyeTbl user = db.uyeTbl.FirstOrDefault(u => u.kullanici_adi.Equals(username));

        //        // Update fields
        //        user.uye_ad = userprofile.uye_ad;
        //        user.uye_soyad = userprofile.uye_soyad;
        //        user.kullanici_adi = userprofile.kullanici_adi;
        //        user.uye_mail = userprofile.kullanici_adi;



        //        db.Entry(user).State = EntityState.Modified;

        //        db.SaveChanges();

        //        return RedirectToAction("profil", "Uye"); // or whatever
        //    }

        //    return View(userprofile);
        //}
    }
}
