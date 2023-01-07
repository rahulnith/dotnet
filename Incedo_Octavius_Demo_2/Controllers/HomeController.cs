using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incedo_Octavius_Demo_2.Controllers
{
    public class HomeController : Controller
    {
        int ta_id;
        int user_type;

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            CheckLogin(username, password);
            if(ta_id==0 && user_type == 2)
            {
                ViewBag.ta_id = Session["ta_id"];
                ViewBag.user_type = Session["user_type"];
                return (RedirectToAction("Index", "BusinessUserDegreeErrorModels"));
            }
            else if (user_type==1)
            {
                ViewBag.ta_id = Session["ta_id"];
                ViewBag.user_type = Session["user_type"];
                return (RedirectToAction("Index", "KOL_Image"));
            }
            ViewBag.ta_id = Session["ta_id"];
            ViewBag.user_type = Session["user_type"];
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Privacy()
        {
            ViewBag.Message = "Your Privacy page.";

            return View();
        }

        public ActionResult Team()
        {
            ViewBag.Message = "Our Team";
            return View();
        }

        public void CheckLogin(string username, string password)
        {   if(password!="1234")
            {
                return;
            }
            if (username == "Lisa")
            {
                ta_id = 1;
                user_type = 1;
            }
            else if (username == "Ari")
            {
                ta_id = 2;
                user_type = 1;
            }
            else if (username == "James")
            {
                ta_id = 0;
                user_type = 2;
            }
            Session["ta_id"] = ta_id;
            Session["user_type"] = user_type;
        }
    }
}