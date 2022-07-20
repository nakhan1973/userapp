using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using userDemo1.Context;
using userDemo1.Models;

namespace userDemo1.Controllers
{
    public class CustomerController : Controller
    {
        private static List<Customer> customerList = null;

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

        public ActionResult Index()
        {
            if (customerList == null)
            {
                customerList = new List<Customer>()
                    {
                        new Customer { Id=1010, FullName="Berry Jonson", Email="b.jonson@gmail.com",Phone="(235) 223 1122", DateOfBirth="12-07-1996",Gender= Gender.Female},
                        new Customer { Id=1214, FullName="Stephan Desoza", Email="s.desoza@yahoo.com",Phone="(335) 823 3411", DateOfBirth="18-07-1993",Gender= Gender.Male},
                        new Customer { Id=1337, FullName="Mark Robert", Email="robert.m@live.com",Phone="(235) 324 5532", DateOfBirth="24-06-1998",Gender= Gender.Male},
                        new Customer { Id=1367, FullName="Susan Jermy", Email="susan.jermy@gmail.com",Phone="(761) 822 9201", DateOfBirth="12-07-1999",Gender= Gender.Female},
                        new Customer { Id=2945, FullName="Mary Michael ", Email="mary.michael@gmail.com",Phone="(533) 112 5576", DateOfBirth="12-07-2000",Gender= Gender.Female}
                    };
            }
            if (IsUserLoggedIn())
            {
                var loggedInUser = (User)Session["LoggedInUser"];
                TempData["ViewAuthorized"] = loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "customer" && x.ViewPermission == true).Any();
                TempData["AddAuthorized"] = loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "customer" && x.AddPermission == true).Any();
                TempData["EditAuthorized"] = loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "customer" && x.EditPermission == true).Any();
                TempData["DeleteAuthorized"] = loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "customer" && x.DeletePermission == true).Any();

                return View(customerList);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Delete(int Id)
        {
            if (IsUserLoggedIn())
            {
                var loggedInUser = (User)Session["LoggedInUser"];
                if (loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "customer" && x.DeletePermission == true).Any())
                {
                    var customerRecord = customerList.Where(x => x.Id == Id).FirstOrDefault();
                    if (customerRecord != null)
                    {
                        customerList.Remove(customerRecord);
                    }
                    return RedirectToAction("Index", customerList);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create(Customer editModel)
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
        public ActionResult AddUpdateCustomer(Customer model)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = (User)Session["LoggedInUser"];

                if (model.Id == 0)
                {
                    if (loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "customer" && x.AddPermission == true).Any())
                    {
                        customerList.Add(model);
                    }
                }
                else
                {
                    if (loggedInUser.UserPermissions.Where(x => x.ModuleName.ToLower() == "customer" && x.EditPermission == true).Any())
                    {
                        Customer customerRecord = customerList.Where(x => x.Id == model.Id).FirstOrDefault();

                        customerRecord.Id = model.Id;
                        customerRecord.FullName = model.FullName;
                        customerRecord.Email = model.Email;
                        customerRecord.Phone = model.Phone;
                        customerRecord.Gender = model.Gender;
                        customerRecord.DateOfBirth = model.DateOfBirth;
                    }
                }
            }
            ModelState.Clear();

            return RedirectToAction("Index", customerList);
        }
    }
}