using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoodOrder.Models;

namespace FoodOrder.Controllers
{
    public class ResCatsController : Controller
    {
        private FoodOrderModel db = new FoodOrderModel();

        // GET: ResCats
        public ActionResult Index()
        {
            var resCat = db.ResCat.Include(r => r.cat).Include(r => r.foodit).Include(r => r.rest);
            return View(resCat.ToList());
        }

        // GET: ResCats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResCat resCat = db.ResCat.Find(id);
            if (resCat == null)
            {
                return HttpNotFound();
            }
            return View(resCat);
        }

        // GET: ResCats/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.FCategory, "CategoryId", "CategoryName");
            ViewBag.FoodId = new SelectList(db.FoodItems, "FoodId", "FoodName");
            ViewBag.RId = new SelectList(db.Restaurant, "RId", "RName");
            return View();
        }

        // POST: ResCats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RId,CategoryId,FoodId,FoodPrice")] ResCat resCat)
        {
            if (ModelState.IsValid)
            {
                db.ResCat.Add(resCat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.FCategory, "CategoryId", "CategoryName", resCat.CategoryId);
            ViewBag.FoodId = new SelectList(db.FoodItems, "FoodId", "FoodName", resCat.FoodId);
            ViewBag.RId = new SelectList(db.Restaurant, "RId", "RName", resCat.RId);
            return View(resCat);
        }

        // GET: ResCats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResCat resCat = db.ResCat.Find(id);
            if (resCat == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.FCategory, "CategoryId", "CategoryName", resCat.CategoryId);
            ViewBag.FoodId = new SelectList(db.FoodItems, "FoodId", "FoodName", resCat.FoodId);
            ViewBag.RId = new SelectList(db.Restaurant, "RId", "RName", resCat.RId);
            return View(resCat);
        }

        // POST: ResCats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RId,CategoryId,FoodId,FoodPrice")] ResCat resCat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resCat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.FCategory, "CategoryId", "CategoryName", resCat.CategoryId);
            ViewBag.FoodId = new SelectList(db.FoodItems, "FoodId", "FoodName", resCat.FoodId);
            ViewBag.RId = new SelectList(db.Restaurant, "RId", "RName", resCat.RId);
            return View(resCat);
        }

        // GET: ResCats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResCat resCat = db.ResCat.Find(id);
            if (resCat == null)
            {
                return HttpNotFound();
            }
            return View(resCat);
        }

        // POST: ResCats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResCat resCat = db.ResCat.Find(id);
            db.ResCat.Remove(resCat);
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
