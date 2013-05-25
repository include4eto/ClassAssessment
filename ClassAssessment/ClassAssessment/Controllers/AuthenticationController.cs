using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClassAssessment.Models;
using HttpContext = System.Web.HttpContext;

namespace ClassAssessment.Controllers
{
	public class AuthenticationController : Controller
	{
		private ClassAssessment.Models.db92115aa68afb4039a247a1c20130a248Entities DBContext =
			new db92115aa68afb4039a247a1c20130a248Entities();

		[Authorize]
		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return View("Login");
		}

		[HttpPost]
		public ActionResult Login(string inputName, string inputPassword)
		{
			ViewBag.Username = inputName;
			var user = (from x in DBContext.Users
					   where x.Name == inputName
					   select x).FirstOrDefault();

			if (user == null)
				return View();

			{
                FormsAuthentication.SetAuthCookie(user.Name + " " + user.Surname, true);
                //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                //    1,
                //    user.Name + " " + user.Surname,
                //    DateTime.Now,
                //    DateTime.Now.AddMinutes(20),
                //    true,
                //    "Member," + user.Roles,
                //    "/");

                //Roles.AddUserToRole(authTicket.Name, user.Roles);

                //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                //                                   FormsAuthentication.Encrypt(authTicket));
                //Response.Cookies.Add(cookie);

				return RedirectToAction("Index", "Default");
			}
		}

		public ActionResult Login()
		{
			if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Default");
			return View();
		}
	}
}
