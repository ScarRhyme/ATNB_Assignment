using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ATNB_Assignment.Models;
using PagedList;

namespace ATNB_Assignment.Controllers
{
    public class OrdersController : Controller
    {
        private ATNB_Assignment_BookEntities db = new ATNB_Assignment_BookEntities();

        // GET: Orders
        public ActionResult Index(int? page)
        {
            if (Session["UserName"] != null)
            {
                var list = db.Orders.OrderBy(p => p.OrderId).ToPagedList(page ?? 1, 9);
                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Where(p=> p.OrderId == id).Include(x => x.Order).FirstOrDefault();
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
