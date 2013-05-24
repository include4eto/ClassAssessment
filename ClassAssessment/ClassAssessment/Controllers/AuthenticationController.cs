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

			//if (inputName == "asdf" && inputPassword == "asdf")
			{
				FormsAuthentication.SetAuthCookie(user.Name + " " + user.Surname, true);

				return RedirectToAction("Index", "Default");
			}
		}

		public ActionResult Login()
		{
			if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Default");
			return View();
		}

		[Authorize(Users="Трайко Динев")]
		public string Dummy()
		{
			return "fuck yeah";
		}
	}
}
