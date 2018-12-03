using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ATNB_Assignment.Models;

namespace ATNB_Assignment.Controllers
{
    public class AuthController : Controller
    {
        private ATNB_Assignment_BookEntities db = new ATNB_Assignment_BookEntities();
        // GET: Auth
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UserName, string Password)
        {
            var obj = db.Users.ToList().Where(u => u.UserName.Equals(UserName) &&  u.Password.Equals(Password)).FirstOrDefault();

            if (obj != null)
            {
                Session["UserName"] = obj.UserName.ToString();
                Session["Email"] = obj.Email.ToString();
                HttpCookie ckus = new HttpCookie("UserName", UserName);
                HttpCookie ckps = new HttpCookie("Password", Password);
                ckus.Expires = DateTime.Now.AddDays(15);
                ckps.Expires = DateTime.Now.AddDays(15);

                Response.Cookies.Add(ckus);
                Response.Cookies.Add(ckps);

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Error = "Account's Invalid";
                return View();
            }
            
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}