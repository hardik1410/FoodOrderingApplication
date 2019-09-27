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
    public class FoodItemsController : Controller
    {
        private FoodOrderModel db = new FoodOrderModel();

        // GET: FoodItems
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("error", "FCategories");
            }
            else if(Session["username"].ToString() == "Admin")
            {
                var foodItems = db.FoodItems.Include(f => f.Cat);
                return View(foodItems.ToList());
                
            }
            else
            {
                return RedirectToAction("error","FCategories");
            }
        }

        // GET: FoodItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItems foodItems = db.FoodItems.Find(id);
            if (foodItems == null)
            {
                return HttpNotFound();
            }
            return View(foodItems);
        }

        // GET: FoodItems/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.FCategory, "CategoryId", "CategoryName");
            return View();
        }

        // POST: FoodItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodId,FoodName,FoodPrice,CategoryId")] FoodItems foodItems)
        {
            if (ModelState.IsValid)
            {
                db.FoodItems.Add(foodItems);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.FCategory, "CategoryId", "CategoryName", foodItems.CategoryId);
            return View(foodItems);
        }

        // GET: FoodItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItems foodItems = db.FoodItems.Find(id);
            if (foodItems == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.FCategory, "CategoryId", "CategoryName", foodItems.CategoryId);
            return View(foodItems);
        }

        // POST: FoodItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoodId,FoodName,FoodPrice,CategoryId")] FoodItems foodItems)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodItems).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.FCategory, "CategoryId", "CategoryName", foodItems.CategoryId);
            return View(foodItems);
        }

        // GET: FoodItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodItems foodItems = db.FoodItems.Find(id);
            if (foodItems == null)
            {
                return HttpNotFound();
            }
            return View(foodItems);
        }

        // POST: FoodItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodItems foodItems = db.FoodItems.Find(id);
            db.FoodItems.Remove(foodItems);
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
