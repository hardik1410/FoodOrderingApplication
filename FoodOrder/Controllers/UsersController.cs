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
    public class UsersController : Controller
    {
        private FoodOrderModel db = new FoodOrderModel();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.msg = user.FirstName + user.LastName + "successfully registered.";
            return View();
        }
        public ActionResult login()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult login(User user)
        {
            var usr = db.Users.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
            if (usr != null)
            {
                Session["UserId"] = usr.UserId.ToString();
                Session["Username"] = usr.Username.ToString();
                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("", "Usrname or password is wrong.");
            }
            return View();
        }

        public ActionResult Loggedin()
        {
            
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("login");
        }
    }
}
