using CDataWeb.Helpers;
using CDataWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CDataWeb.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult SetCulture(string culture)
        {
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }

        public static bool SendGmail(string subject, string content, string[] recipients, string from)
        {
            if (recipients == null || recipients.Length == 0)
                throw new ArgumentException("recipients");

            var gmailClient = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("******", "*****")
            };

            using (var msg = new System.Net.Mail.MailMessage(from, recipients[0], subject, content))
            {
                for (int i = 1; i < recipients.Length; i++)
                    msg.To.Add(recipients[i]);

                try
                {
                    gmailClient.Send(msg);
                    return true;
                }
                catch (Exception)
                {
                    // TODO: Handle the exception
                    return false;
                }
            }
        }

    }
}