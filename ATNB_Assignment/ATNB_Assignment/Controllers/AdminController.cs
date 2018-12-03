using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATNB_Assignment.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }
    }
}