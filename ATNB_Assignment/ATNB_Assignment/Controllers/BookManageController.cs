using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using ATNB_Assignment.Models;
using System.IO;

namespace ATNB_Assignment.Controllers
{
    public class BookManageController : Controller
    {
        private ATNB_Assignment_BookEntities db = new ATNB_Assignment_BookEntities();

        // GET: BookManage
        public ActionResult Index(int? page)
        {
            if (Session["UserName"] != null)
            {
                var books = db.Books.Include(b => b.Author).Include(b => b.Category).Include(b => b.Publisher).OrderBy(p => p.BookId).ToPagedList(page ?? 1, 10);
                return View(books);
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        // GET: BookManage/Details/5
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

        // GET: BookManage/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName");
            ViewBag.CateId = new SelectList(db.Categories, "CateId", "CateName");
            ViewBag.PubId = new SelectList(db.Publishers, "PubId", "Name");
            return View();
        }

        // POST: BookManage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,Title,CateId,AuthorId,PubId,Summary,Price,Quantity,IsActive")] Book book, HttpPostedFileBase ImgUrl)
        {
            if (ModelState.IsValid)
            {
                book.CreatedDate = DateTime.Now;

                string file = Path.Combine(Server.MapPath("~/Content/Images/"), Path.GetFileName(ImgUrl.FileName));
                ImgUrl.SaveAs(file);
                book.ImgUrl = "/Content/Images/" + ImgUrl.FileName;

                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", book.AuthorId);
            ViewBag.CateId = new SelectList(db.Categories, "CateId", "CateName", book.CateId);
            ViewBag.PubId = new SelectList(db.Publishers, "PubId", "Name", book.PubId);
            return View(book);
        }

        // GET: BookManage/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", book.AuthorId);
            ViewBag.CateId = new SelectList(db.Categories, "CateId", "CateName", book.CateId);
            ViewBag.PubId = new SelectList(db.Publishers, "PubId", "Name", book.PubId);
            return View(book);
        }

        // POST: BookManage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase ImgUrl, int id, Book book)
        {
            if (ModelState.IsValid)
            {
                var bookUpdate = db.Books.Find(id);
                //Upload image
                if (book.ImgUrl != null)
                {
                    string file = Path.Combine(Server.MapPath("~/Content/Images/"), Path.GetFileName(ImgUrl.FileName));
                    ImgUrl.SaveAs(file);
                    bookUpdate.ImgUrl = "/Content/Images/" + ImgUrl.FileName;

                }
                else
                {
                    book.ImgUrl = bookUpdate.ImgUrl;
                }
                bookUpdate.Title = book.Title;
                bookUpdate.AuthorId = book.AuthorId;
                bookUpdate.CateId = book.CateId;
                bookUpdate.Publisher = book.Publisher;
                bookUpdate.Summary = book.Summary;
                bookUpdate.Price = book.Price;
                bookUpdate.Quantity = book.Quantity;
                bookUpdate.IsActive = book.IsActive;
                bookUpdate.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", book.AuthorId);
            ViewBag.CateId = new SelectList(db.Categories, "CateId", "CateName", book.CateId);
            ViewBag.PubId = new SelectList(db.Publishers, "PubId", "Name", book.PubId);
            return View(book);
        }

        // GET: BookManage/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: BookManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
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
