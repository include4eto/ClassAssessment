using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ClassAssessment.Controllers
{
	public class TestController : Controller
	{
		//
		// GET: /Test/

		[Authorize]
		public string Index()
		{
			FormsAuthentication.SignOut();
			return "Fuck you, world";
		}

		[HttpPost]
		public ActionResult Login(string inputName, string inputPassword)
		{
			if (inputName == "asdf" && inputPassword == "asdf")
			{
				FormsAuthentication.SetAuthCookie(inputName, true);
				return RedirectToAction("Index");
			}
			else return View("~/Views/Login.cshtml");
		}

		public ActionResult Login()
		{
			return View("~/Views/Login.cshtml");
		}
	}
}
