using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Ootan sind minu peole! Palun Tule!!!";
            int hour = DateTime.Now.Hour;
            ViewBag.Greting = hour < 10 ? "Tere hommikust!" : "Tere päevast!";
            return View();
        }

        [HttpGet]

        public ViewResult Ankeet()
        {
            return View();
        }
    }
}