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
    public class resclass
    {
        public string name { get; set; }
    } 
    public class rcat
    {
        public int rid { get; set; }
        public string rname { get; set; }
        public string fname { get; set; }
        public int price { get; set; }
    }
    public class Rest
    {
        public string rname { get; set; }
        public string loc { get; set; }
        public string contact { get; set; }
        public string time { get; set; }
    }
    public class category
    {
        public string cname { get; set; }
    }
    public class data
    {
        public int rid { get; set;}
        public string rname { get; set; }
        public string fname { get; set; }
        public int price { get; set; }
        public int fid { get; set; }
    }

    public class food
    {
        public string foodname { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public int fid { get; internal set; }
    } 
    public class RestaurantsController : Controller
    {
        private FoodOrderModel db = new FoodOrderModel();

        public List<food> data1;




        // GET: Restaurants
        public ActionResult Index()
        {
            if(Session["username"] == null)
            {
                TempData["message"] = "you are not allowed to access this page.";
                return RedirectToAction("errorlist");
            }
            else if (Session["username"].ToString() == "Admin")
            {
                return View(db.Restaurant.ToList());
            }
            else
            {
                TempData["message"] = "you are not allowed to access this page.";
                return RedirectToAction("errorlist");
            }
        }
        public ActionResult errorlist()
        {
            return View();
        }
        
        // GET: Restaurants/Details/5
        public ActionResult Details(int? id)
        {
            if(Session["username"] == null)
            {
                TempData["message"] = "you are not allowed to access this page.";
                return RedirectToAction("errorlist");
            }
            else if (Session["username"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Restaurant restaurant = db.Restaurant.Find(id);
                if (restaurant == null)
                {
                    return HttpNotFound();
                }
                return View(restaurant);
            }
            else
            {
                TempData["message"] = "you are not allowed to access this page.";
                return RedirectToAction("errorlist");
            }
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            if(Session["username"] == null)
            {
                TempData["message"] = "you are not allowed to access this page.";
                return RedirectToAction("errorlist");
            }
            else if (Session["username"].ToString() == "Admin")
            {
                return View();
            }
            else
            {
                TempData["message"] = "you are not allowed to access this page.";
                return RedirectToAction("errorlist");
            }
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RId,RName,RLocation,RContact,OpenCloseTime")] Restaurant restaurant)
        {
            if(Session["username"] == null)
            {
                TempData["message"] = "you are not allowed to access this page.";
                return RedirectToAction("errorlist");
            }
            else if (Session["username"].ToString() == "Admin")
            {
                if (ModelState.IsValid)
                {
                    db.Restaurant.Add(restaurant);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["message"] = "you are not allowed to access this page.";
                return RedirectToAction("errorlist");
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if(Session["username"] == null)
            {
                TempData["message"] = "you are not allowed to access this page.";
                return RedirectToAction("errorlist");
            }
            else if (Session["username"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Restaurant restaurant = db.Restaurant.Find(id);
                if (restaurant == null)
                {
                    return HttpNotFound();
                }
                return View(restaurant);
            }
            else
            {
                TempData["message"] = "you are not allowed to access this page.";
                return RedirectToAction("errorlist");
            }
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RId,RName,RLocation,RContact,OpenCloseTime")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["username"] == null)
            {
                TempData["message"] = "you are not allowed to access this page.";
                return RedirectToAction("errorlist");
            }
            else if(Session["username"].ToString() == "Admin")
            { 
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Restaurant restaurant = db.Restaurant.Find(id);
                if (restaurant == null)
                {
                    return HttpNotFound();
                }
                return View(restaurant);
            }
            else
            {
                TempData["message"] = "you are not allowed to access this page.";
                return RedirectToAction("errorlist");
            }
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurant.Find(id);
            db.Restaurant.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Retrive()
        {
            var rest = (from r in db.Restaurant
                        select new Rest() {
                            rname = r.RName,
                            loc=r.RLocation,
                            contact=r.RContact,
                            time=r.OpenCloseTime
                        }).ToList();
            ViewBag.rest = rest;
            return View();
        }
        
        public ActionResult Category(string rname)
        {
            if(Request["flist"]=="")
            {
                return RedirectToAction("error");
            }
            int re = Convert.ToInt32(Request["flist"]);
            Session["RestaurantID"] = Convert.ToInt32(Request["flist"]);
            TempData["data"] = re;

            var data = (from r in db.ResCat  
                       where r.RId == re
                        join c in db.FCategory on r.CategoryId equals c.CategoryId
                        join f in db.FoodItems on r.FoodId equals f.FoodId
                        select new data() {
                        fname =f.FoodName,
                        rname=c.CategoryName,
                        price=r.FoodPrice,
                        fid=r.FoodId,
                        rid=r.RId
                       }).ToList();
            var cat = (from c in db.FCategory
                       select new category() { cname=c.CategoryName}).ToList();
            ViewBag.category = cat; 
            ViewBag.values = data;
            return View();
        }
        Dictionary<int, List<food>> dictionary = new Dictionary<int, List<food>>();
        public ActionResult cart()
        {
            int Resid = Convert.ToInt32(Session["RestaurantID"]);
                var id = from f in db.FoodItems
                         select f;
            int sum = 0;
                foreach (var item in id)
                {
                    var x = Convert.ToString(item.FoodId);
                    if(Request[x]=="")
                    {
                    continue;
                    }
                    sum = sum + Convert.ToInt32(Request[x]);
                    
                }
            if (sum==0)
            {
                return RedirectToAction("error");
            }
            foreach (var item in id)
                {
                    var x = Convert.ToString(item.FoodId);
                if (Request[x] == "")
                {
                    continue;
                }
                var y = Convert.ToInt32(Request[x]);

                    var foodid = Convert.ToInt32(x);
                    TempData["x"] = x;
                    TempData["y"] = y;
                    if (y > 0)
                    {

                        data1 = ((from r in db.ResCat
                                  where r.FoodId==foodid && r.RId == Resid
                                  join fi in db.FoodItems on r.FoodId equals fi.FoodId
                        
                                    /*from c in db.FoodItems
                                  where c.FoodId == foodid 
                                  join r in db.ResCat on Resid equals r.RId*/
                                  
                                  select new food()
                                  {
                                      foodname = fi.FoodName,
                                      quantity = y,
                                      price = r.FoodPrice * y
                                  }).ToList());
                        dictionary.Add(item.FoodId, data1);
                    }
                }
                Session["order"] = dictionary;
                ViewBag.values = dictionary;
                return View();
            
            
        }

       public ActionResult Order(FormCollection f)
        {
            if (Session["username"] != null)
            {
                Dictionary<int, List<food>> obj = (Dictionary<int, List<food>>)TempData["d"];
                ViewBag.orders = obj;
                TempData["t"] = Request["Total"];
                TempData["c"] = Request["count"];
                return View();
            }
            else
            {
                return RedirectToAction("login", "Users");
            }

        }

        public ActionResult placed()
        {
            TempData["a"] = Request["address"];
            return View();
        }
        public ActionResult error()
        {
            TempData["msg"] = "Please select an item.";
            return View();
        }


        /*category*/

        public ActionResult Retrivecat()
        {
            return View();
        }


        public ActionResult selectcat()
        {
            if(Request["flist"]=="")
            {
                return RedirectToAction("errorcat");
            }
            int re = Convert.ToInt32(Request["flist"]);
            TempData["data"] = re;
            Session["CategoryID"] = re;

            var data = (from r in db.ResCat
                        where r.CategoryId == re 
                        join c in db.Restaurant on r.RId equals c.RId
                        select new rcat()
                        {
                            
                            rname = c.RName,
                            rid=c.RId
                            
                        }).Distinct().ToList();
            
            ViewBag.restaurant = data;
            
            return View();
        }

        public ActionResult errorcat()
        {
            TempData["msg"] = "PLEASE SELECT A CATEGORY.";
            return View();
        }


        public ActionResult catcart()
        {

            int Resid = Convert.ToInt32(Session["id"]);
            var id = from f in db.FoodItems
                     select f;

            int sum = 0;
            foreach (var item in id)
            {
                var x = Convert.ToString(item.FoodId);
                if (Request[x] == "")
                {
                    continue;
                }
                sum = sum + Convert.ToInt32(Request[x]);

            }
            if (sum == 0)
            {
                return RedirectToAction("errorcat");
            }



            foreach (var item in id)
            {
                var x = Convert.ToString(item.FoodId);
                if (Request[x] == "")
                {
                    continue;
                }
                var y = Convert.ToInt32(Request[x]);

                var foodid = Convert.ToInt32(x);
                TempData["x"] = x;
                TempData["y"] = y;
                if (y > 0)
                {

                    data1 = ((from r in db.ResCat
                              where r.FoodId == foodid && r.RId == Resid
                              join fi in db.FoodItems on r.FoodId equals fi.FoodId

                              /*from c in db.FoodItems
                            where c.FoodId == foodid 
                            join r in db.ResCat on Resid equals r.RId*/

                              select new food()
                              {
                                  foodname = fi.FoodName,
                                  quantity = y,
                                  price = r.FoodPrice * y
                              }).ToList());
                    dictionary.Add(item.FoodId, data1);
                }
            }
            Session["order"] = dictionary;
            ViewBag.values = dictionary;
            return View();


        }

        public ActionResult foods(int? id)
        {
            int cid = Convert.ToInt32(Session["CategoryID"]);
            Session["id"] = id;
            var foods = ((from r in db.ResCat
                         where r.RId == id && r.CategoryId==cid
                         join fi in db.FoodItems on r.FoodId equals fi.FoodId
                      select new food()
                      {
                          fid=r.FoodId,
                          foodname = fi.FoodName,
                          price = r.FoodPrice,
                      }).ToList());
            ViewBag.foods = foods;
            return View();
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
