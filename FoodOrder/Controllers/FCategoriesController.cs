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
    public class FCategoriesController : Controller
    {
        private FoodOrderModel db = new FoodOrderModel();

        // GET: FCategories
        public ActionResult Index()
        {
            if (Session["username"].ToString() == "Admin")
            {
                return View(db.FCategory.ToList());
            }
            else
            {
                return RedirectToAction("error");
            }
        }
        public ActionResult error()
        {
            return View();
        }
       

        // GET: FCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["username"].ToString() == "Admin")
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                FCategory fCategory = db.FCategory.Find(id);
                if (fCategory == null)
                {
                    return HttpNotFound();
                }
                return View(fCategory);
            }
            else
            {
                return RedirectToAction("error");
            }
        }

        // GET: FCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] FCategory fCategory)
        {
            if (ModelState.IsValid)
            {
                db.FCategory.Add(fCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fCategory);
        }

        // GET: FCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FCategory fCategory = db.FCategory.Find(id);
            if (fCategory == null)
            {
                return HttpNotFound();
            }
            return View(fCategory);
        }

        // POST: FCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] FCategory fCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fCategory);
        }

        // GET: FCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FCategory fCategory = db.FCategory.Find(id);
            if (fCategory == null)
            {
                return HttpNotFound();
            }
            return View(fCategory);
        }

        // POST: FCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FCategory fCategory = db.FCategory.Find(id);
            db.FCategory.Remove(fCategory);
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
