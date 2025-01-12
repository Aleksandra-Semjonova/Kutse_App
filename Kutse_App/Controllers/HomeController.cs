﻿using Kutse_App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {

        private static readonly Dictionary<int, string> Pidu = new Dictionary<int, string>
        {
            {1, "Head uut aastat!"},{2, "Head Eesti iseseisvuspäeva!"},{12, "Haid jõule"}
        };

        public ActionResult Index()
        {
            string greeting;



            int month = DateTime.Now.Month;
            int hour = DateTime.Now.Hour;

            if (hour >= 5 && hour < 12)
            {
                greeting = "Tere hommikust!!!";
            }
            else if (hour >= 12 && hour < 18)
            {
                greeting = "Tere päevast!!!";
            }
            else if (hour >= 18 && hour < 22)
            {
                greeting = "Tere õhtust!!!";
            }
            else 
            {
                greeting = "Head ööd!!!";
            }

            ViewBag.Greeting = greeting;
            string holidayMessage = Pidu.ContainsKey(month) ? Pidu[month] : "";

            ViewBag.Greeting = greeting + (string.IsNullOrEmpty(holidayMessage) ? "" : " " + holidayMessage);
            ViewBag.Message = "Ootan sind minu peole! Palun tule!";
            return View();
        }

        [HttpGet]

        public ViewResult Ankeet()
        {
            return View();
        }

        [HttpPost]


        public ViewResult Ankeet(Guest guest)
        {
            if (ModelState.IsValid)
            {
                E_mail(guest);
                if (ModelState.IsValid)
                {
                    return View("Thanks", guest);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View(guest);
            }
        }

        public void E_mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "aleksandra.semjonova24@gmail.com";
                WebMail.Password = "iqer zkvm czuv lgqn";
                WebMail.From = "aleksandra.semjonova24@gmail.com";
                WebMail.Send(guest.Email, " Vastus kutsele ", guest.Name + " vastas "
                    + ((guest.WillAttend ?? false ? " tuleb peole" : " ei tule saatnud"))); ViewBag.Message = "Kiri on saatnud!";
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahju!Ei saa kirja saada!!!";
            }
        }
        [HttpPost]

        //Добавьте кнопку "Отправить напоминание/Meeldetuletus", по нажатию на которую должно отправиться
        public ActionResult Meeldetuletus(Guest guest, string Meeldetuletus)
        {
            if (!string.IsNullOrEmpty(Meeldetuletus))
            {
                try
                {
                    WebMail.SmtpServer = "smtp.gmail.com";
                    WebMail.SmtpPort = 587;
                    WebMail.EnableSsl = true;
                    WebMail.UserName = "aleksandra.semjonova24@gmail.com";
                    WebMail.Password = "lsrs danp cdwm ogmd ";
                    WebMail.From = "aleksandra.semjonova24@gmail.com";

                    WebMail.Send(guest.Email, "Meeldetuletus", guest.Name + ", ara unusta.",
                    null, "aleksandra.semjonova24@gmail.com",
                    filesToAttach: new String[] { Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName("happy.png ")) }
                   );

                    ViewBag.Message = "Kutse saadetud!";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Tekkis viga kutse saatmisel: " + ex.Message;
                }
            }

            return View("Thanks", guest);
        }
    }
}