using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagementSystem.Controllers
{
    
    public class LoginController : Controller
    {
        StudentManagementSystemEntities db = new StudentManagementSystemEntities();



        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //Post: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(user objchk)
        {
            if(ModelState.IsValid)
            {
                using(StudentManagementSystemEntities db = new StudentManagementSystemEntities())
                {
                    var obj = db.users.Where(a => a.firstname.Equals(objchk.firstname) && a.password.Equals(objchk.password)).FirstOrDefault();

                    if (obj != null)
                    {
                        Session["UserID"] = obj.id.ToString();
                        Session["Firstname"] = obj.firstname.ToString();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The username or password is incorrect, Please try again");
                    }
                }
                
            }

            return View(objchk);
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}