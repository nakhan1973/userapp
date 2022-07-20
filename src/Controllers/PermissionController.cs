using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using userDemo1.Context;

namespace userDemo1.Controllers
{
    public class PermissionController : Controller
    {
        dbtestEntities _dbContext = new dbtestEntities();
        // GET: Permission
        public ActionResult Index(int userId)
        {
            User user = GetUserInformation(userId);
            Session.Add("User", user);

            return View(user);
        }

        private User GetUserInformation(int userId)
        {
            var test = _dbContext.Users.Include("UserPermissions").Where(x => x.UserId == userId).FirstOrDefault();
            //var user = _dbContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
            //var permission = _dbContext.UserPermissions.Where(p => p.UserId == userId).ToList();
            //user.UserPermissions = permission;
            return test;
        }

        public ActionResult Create(UserPermission editModel,int userId)
        {
            if (editModel == null)
            {
                editModel = new UserPermission();
                editModel.UserId = userId;
            }
            return View(editModel);
        }

        [HttpPost]
        public ActionResult AddUpdatePermission(UserPermission model)
        {
            if (ModelState.IsValid)
            {
                UserPermission userPermission = new UserPermission();

                userPermission.Id = model.Id;
                userPermission.ModuleName = model.ModuleName;
                userPermission.ViewPermission = model.ViewPermission;
                userPermission.AddPermission = model.AddPermission;
                userPermission.EditPermission = model.EditPermission;
                userPermission.DeletePermission = model.DeletePermission;
                userPermission.UserId = model.UserId;

                if (model.Id == 0)
                {
                    _dbContext.UserPermissions.Add(userPermission);
                }
                else
                {
                    _dbContext.Entry(userPermission).State = System.Data.Entity.EntityState.Modified;
                }
                _dbContext.SaveChanges();
            }
            ModelState.Clear();
            User user = GetUserInformation(model.UserId);
            return RedirectToAction("Index", user);
        }

        public ActionResult Delete(int Id)
        {
            var userPermission = _dbContext.UserPermissions.Where(x => x.Id == Id).FirstOrDefault();
            if (userPermission != null)
            {
                _dbContext.UserPermissions.Remove(userPermission);
                _dbContext.SaveChanges();
            }
            User user = GetUserInformation(userPermission.UserId);
            return RedirectToAction("Index", user);
        }

    }
}