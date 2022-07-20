using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using userDemo1.Context;
using userDemo1.Models;

namespace userDemo1.Controllers
{
    public class HomeController : Controller
    {
        dbtestEntities _dbContext = new dbtestEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["LoggedInUser"] = null;
            return View("Index");
        }

        public ActionResult Login(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                var authenticatedUser = _dbContext.Users.Include("UserPermissions").Where(u => u.Email == userLogin.UserId && u.Password == userLogin.Password).FirstOrDefault();
                if (authenticatedUser != null)
                {
                    Session["LoggedInUser"] = null;
                    Session["LoggedInUser"] = authenticatedUser;

                    return RedirectToAction("Index","Users");
                }
                else
                {
                    ModelState.AddModelError("LoginFailed", "Invalid Login Id or Password!");
                    return View("Index");
                }
            }
            return View();
        }
    }
}