using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodOrder.Models;

namespace FoodOrder.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: Acc
        private FoodOrderModel1 db = new FoodOrderModel1();
        
        public ActionResult Index()
        {
            
            return View(db.UserAccounts.ToList());
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount user)
        {
            if(ModelState.IsValid)
            {
                db.UserAccounts.Add(user);
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
        public ActionResult login(UserAccount user)
        {
            var usr = db.UserAccounts.Where(u => u.username == user.username && u.Password == user.Password).FirstOrDefault();
            if (usr != null)
            {
                Session["UserId"] = usr.UserId.ToString();
                Session["Username"] = usr.username.ToString();
                return RedirectToAction("Loggedin");
            }
            else
            {
                ModelState.AddModelError("","Usrname or password is wrong.");
            }
            return View();
        }

        public ActionResult Loggedin()
        {
            if(Session["UserId"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login");
            }
        }
    }
}