using InvoiceSystem.WebApplication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceSystem.WebApplication.Controllers
{
    [Authorize(Roles ="Admin")]

    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        //[Authorize(Roles ="Admin")]
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(FormCollection formCollection)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string userName = formCollection["txtEmail"];
            string email = formCollection["txtEmail"];
            string pwd = formCollection["txtPwd"];
            string fullName = formCollection["txtFullName"];

            var user = new ApplicationUser();
            user.UserName = userName;
            user.Email = email;
            user.FullName = fullName;
            var newUser = userManager.Create(user, pwd);
            
            if (newUser.Succeeded)
            {
                userManager.AddToRole(user.Id, "Employee");
            }

            return View();
        }
        public ActionResult AssignRole()
        {
            ViewBag.Roles = context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult AssignRole(FormCollection formCollection)
        {
            string userName = formCollection["txtUserName"];
            string roleName = formCollection["RoleName"];
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            
            userManager.AddToRole(user.Id, roleName);
            
            return View("Index");
        }
    }
}