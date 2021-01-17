
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proje_a.Models;
using proje_a.Models.EntityFramework;
using proje_a.Controllers;
using System.Data.Entity.Core.Metadata.Edm;
using System.Net.Mail;
using System.Net;

namespace proje_a.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        siteDbEntities6 db = new siteDbEntities6();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult iletisim()
        {
            return View();
        }
        [HttpPost]
        public ActionResult iletisim(string uye_ad, string uye_mail, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("didemkaymaz99@gmail.com", "Sender");
                    var receivereEmail = new MailAddress("didemkymz9@gmail.com", "Receiver");
                    var password = "8327107739dk";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receivereEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();

                }
            }
            catch (Exception)
            {

                ViewBag.Error = "Mail Gönderilirken bir problem yaşandı";
            }
            return View();
        }
    }

}