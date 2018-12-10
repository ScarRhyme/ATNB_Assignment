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

        // GET: Store/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName");
            ViewBag.CateId = new SelectList(db.Categories, "CateId", "CateName");
            ViewBag.PubId = new SelectList(db.Publishers, "PubId", "Name");
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,Title,CateId,AuthorId,PubId,Summary,ImgUrl,Price,Quantity,CreatedDate,ModifiedDate,IsActive")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", book.AuthorId);
            ViewBag.CateId = new SelectList(db.Categories, "CateId", "CateName", book.CateId);
            ViewBag.PubId = new SelectList(db.Publishers, "PubId", "Name", book.PubId);
            return View(book);
        }

        // GET: Store/Edit/5
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

        // POST: Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Title,CateId,AuthorId,PubId,Summary,ImgUrl,Price,Quantity,CreatedDate,ModifiedDate,IsActive")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "AuthorId", "AuthorName", book.AuthorId);
            ViewBag.CateId = new SelectList(db.Categories, "CateId", "CateName", book.CateId);
            ViewBag.PubId = new SelectList(db.Publishers, "PubId", "Name", book.PubId);
            return View(book);
        }

        // GET: Store/Delete/5
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

        // POST: Store/Delete/5
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
