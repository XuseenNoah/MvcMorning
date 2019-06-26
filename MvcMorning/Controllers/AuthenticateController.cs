using MvcMorning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcMorning.Controllers
{
    public class AuthenticateController : Controller
    {
        Login repository = new Login();
        // GET: Authenticate
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var User = repository.LoginEnter(login);
                if (User != null)
                {
                    Session["User"] = User;
                    FormsAuthentication.SetAuthCookie(User.Username, false);
                    return RedirectToAction("Index", "HOme");
                }
                else
                {
                    TempData["Eror"] = "Eror";
                }
            }
            return View(login);
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}