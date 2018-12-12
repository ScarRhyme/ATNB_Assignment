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
    public class StoreController : Controller
    {
        private ATNB_Assignment_BookEntities db = new ATNB_Assignment_BookEntities();
        private string strCart = "Cart";
        /*
         * 
         */
        public ActionResult CategoriesCollapse()
        {
            var categories = db.Categories.ToList();
            return PartialView(categories);
        }

        public ActionResult AuthorsCollapse()
        {
            var authors = db.Authors.ToList();
            return PartialView(authors);
        }

        public ActionResult PublisherCollapse()
        {
            var publisher = db.Publishers.ToList();
            return PartialView(publisher);
        }

        // GET: Store
        public ActionResult Index(int? page)
        {
            var books = db.Books.Include(b => b.Author).Include(b => b.Category).Include(b => b.Publisher).Where(p => p.IsActive != false).OrderBy(p => p.BookId).ToPagedList(page ?? 1, 9);
            return View(books);
        }

        // GET: Store/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        /*
         * Search from Sidebar START
         */
        //Find by author id
        public ActionResult FindByAuthId(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var findByAuthId = db.Books.Include(b => b.Author).Where(p => p.IsActive != false && p.AuthorId == id).OrderBy(p => p.Title).ToPagedList(page ?? 1, 9);
            TempData["Author"] = findByAuthId;
            TempData.Remove("Category");
            TempData.Remove("Publish");
            return RedirectToAction("Result", new { Author = id });
        }

        //Find by category id
        public ActionResult FindByCateId(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var findByCateId = db.Books.Include(b => b.Author).Where(p => p.IsActive != false && p.CateId == id).OrderBy(p => p.Title).ToPagedList(page ?? 1, 9);
            TempData["Category"] = findByCateId;
            TempData.Remove("Author");
            TempData.Remove("Publish");
            return RedirectToAction("Result", new { Category = id });
        }

        //Find by publisher id
        public ActionResult FindByPubId(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var findByPubId = db.Books.Include(b => b.Author).Where(p => p.IsActive != false && p.PubId == id).OrderBy(p => p.Title).ToPagedList(page ?? 1, 9);
            TempData["Publish"] = findByPubId;
            TempData.Remove("Author");
            TempData.Remove("Category");
            return RedirectToAction("Result", new { Publisher = id });
        }

        //Result from Sidebar
        public ActionResult Result(int? page)
        {
            if (TempData["Author"] != null)
            {
                TempData.Keep("Author");
                return View(TempData["Author"]);

            }
            else if (TempData["Category"] != null)
            {
                TempData.Keep("Category");
                return View(TempData["Category"]);
            }
            else if (TempData["Publish"] != null)
            {
                TempData.Keep("Publish");
                return View(TempData["Publish"]);
            }
            else
            {
                return HttpNotFound();
            }
        }

        /*
         * Search from Sidebar END
         */

        //Result from Search Form
        [HttpPost]
        public ActionResult Result(int? page, string Search, string currentFilter)
        {
            if (Search != null)
            {
                page = 1;
            }
            else
            {
                Search = currentFilter;

            }

            ViewBag.currentFilter = Search;
            var books = db.Books.Where(x => x.Title.Contains(Search)).OrderBy(x => x.BookId).ToPagedList(page ?? 1, 9);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        /*
         * Cart START
         */

        public ActionResult CustomerCart()
        {
            var list = (List<CartItems>)Session[strCart];
            return View(list);
        }

        [HttpPost]
        public List<CartItems> AddCustomerCart(int id, int quantity)
        {
            if (Session[strCart] == null)
            {
                List<CartItems> listCart = new List<CartItems>{
                    new CartItems(db.Books.Find(id),quantity) };
                Session[strCart] = listCart;
            }
            else
            {
                List<CartItems> listCart = (List<CartItems>)Session[strCart];
                int check = isExistsCheck(id);
                if (check == -1)
                {
                    listCart.Add(new CartItems(db.Books.Find(id), quantity));
                }
                else
                {
                    //listCart[check].Quantity++;
                    Session[strCart] = listCart;
                }

            }
            return (List<CartItems>)Session[strCart];

        }

        public int isExistsCheck(int? id)
        {
            List<CartItems> listCart = (List<CartItems>)Session[strCart];
            for (int i = 0; i < listCart.Count; i++)
            {
                if (listCart[i].Book.BookId == id) return i;
            }
            return -1;
        }

        public int GetAmountItems()
        {
            var list = (List<CartItems>)Session[strCart];
            if (list == null)
            {
                return 0;
            }
            else
            {
                int amount = list.ToList().Count;
                return amount;
            }
        }

        public ActionResult DeleteCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            int check = isExistsCheck(id);
            List<CartItems> listCart = (List<CartItems>)Session[strCart];
            listCart.RemoveAt(check);
            return RedirectToAction("CustomerCart");
        }

        /*
         * Cart END
         */

        /*
        * Order START
        */
        public ActionResult ProcessOrder(FormCollection formCollection)
        {
            List<CartItems> listCart = (List<CartItems>)Session[strCart];
            Order order = new Order()
            {
                CustomerName = formCollection["CustomerName"],
                CustomerEmail = formCollection["CustomerEmail"],
                CustomerPhone = Convert.ToInt32(formCollection["CustomerPhone"]),
                OrderDate = DateTime.Now,
                ShippedDate = DateTime.Now,
                ShipAddress = formCollection["ShipAddress"]
            };
            db.Orders.Add(order);
            db.SaveChanges();

            foreach (CartItems cart in listCart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    BookId = cart.Book.BookId,
                    Quantity = cart.Quantity,
                    UnitPrice = cart.Book.Price,
                    TotalMoney = cart.Book.Price * cart.Quantity,
                    Note = ""
                };
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }
            Session.Remove(strCart);
            return View("CustomerCart");
        }

        /*
        * Order END
        */

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
