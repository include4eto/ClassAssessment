using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClassAssessment.Models;
using HttpContext = System.Web.HttpContext;
using ClassAssessment.Helpers;

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
            var hash = Statistics.GetMD5Hash(inputPassword);
			ViewBag.Username = inputName;
			var user = (from x in DBContext.Users
					   where (x.Name == inputName || x.Surname == inputName) && x.Password == hash
					   select x).FirstOrDefault();

			if (user == null)
				return View();

            FormsAuthentication.SetAuthCookie(user.Name + " " + user.Surname, true);

			return RedirectToAction("Index", "Default");
		}

		public ActionResult Login()
		{
			if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Default");
			return View();
		}
	}
}
