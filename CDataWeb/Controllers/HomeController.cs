﻿using CDataWeb.Helpers;
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

        [HttpPost]
        public ActionResult SendEmail(HomeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model); //RedirectToAction("Index");
            }

            string recipients = "info@cdata.sk";

            var gmailClient = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("info@cdata.sk", "")
            };

            using (var msg = new System.Net.Mail.MailMessage(model.Email, recipients,model.Name + " " + model.Subject, model.Message))
            {
                try
                {
                    gmailClient.Send(msg);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                    return View("Index", model);
                }              
            }

            return RedirectToAction("Index");
        }

    }
}