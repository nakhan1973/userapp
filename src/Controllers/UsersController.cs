using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using userDemo1.Context;

namespace userDemo1.Controllers
{
    public class UsersController : Controller
    {
        dbtestEntities _dbContext = new dbtestEntities();


        // GET: Users
        public ActionResult Index()
        {
            if (IsUserLoggedIn())
            {
                var loggedInUser = (User)Session["LoggedInUser"];
                TempData["ViewAuthorized"] = loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "users" && x.ViewPermission == true).Any();
                TempData["AddAuthorized"] = loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "users" && x.AddPermission == true).Any();
                TempData["EditAuthorized"] = loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "users" && x.EditPermission == true).Any();
                TempData["DeleteAuthorized"] = loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "users" && x.DeletePermission == true).Any();

                var usrList = _dbContext.Users.ToList();
                return View(usrList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private bool IsUserLoggedIn()
        {
            if (Session["LoggedInUser"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        public ActionResult Create(User editModel)
        {
            if (IsUserLoggedIn())
            {
                return View(editModel);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddUpdateUser(User model)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = (User)Session["LoggedInUser"];
                
                User userRecord = new User();

                userRecord.UserId = model.UserId;
                userRecord.FirstName = model.FirstName;
                userRecord.LastName = model.LastName;
                userRecord.Phone = model.Phone;
                userRecord.Email = model.Email;
                userRecord.Password = model.Password;
                userRecord.Status = model.Status;

                if (model.UserId == 0)
                {
                    if (loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "users" && x.AddPermission == true).Any())
                    {
                        _dbContext.Users.Add(userRecord);
                        _dbContext.SaveChanges();
                    }
                }
                else
                {
                    if (loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "users" && x.EditPermission == true).Any())
                    {
                        _dbContext.Entry(userRecord).State = System.Data.Entity.EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                } 
            }
            ModelState.Clear();
            var userList = _dbContext.Users.ToList();
            return RedirectToAction("Index", userList);
        }

        public ActionResult Delete(int UserId)
        {
            if (IsUserLoggedIn())
            {
                var loggedInUser = (User)Session["LoggedInUser"];
                if (loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "users" && x.DeletePermission == true).Any())
                {
                    var userRecord = _dbContext.Users.Where(x => x.UserId == UserId).FirstOrDefault();
                    if (userRecord != null)
                    {
                        _dbContext.Users.Remove(userRecord);
                        _dbContext.SaveChanges();
                    }
                    var userList = _dbContext.Users.ToList();
                    //return View("Index", userList);
                    return RedirectToAction("Index", userList);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}