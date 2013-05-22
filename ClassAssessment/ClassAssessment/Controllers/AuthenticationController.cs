using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ClassAssessment.Controllers
{
	public class AuthenticationController : Controller
	{
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
			if (inputName == "asdf" && inputPassword == "asdf")
			{
				FormsAuthentication.SetAuthCookie(inputName, true);

				return RedirectToAction("Index", "Default");
			}
			else return View();
		}

		public ActionResult Login()
		{
			return View();
		}
	}
}
