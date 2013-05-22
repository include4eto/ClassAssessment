using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassAssessment.Models;

namespace ClassAssessment.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/
		private ClassAssessment.Models.db92115aa68afb4039a247a1c20130a248Entities DBContext =
			new db92115aa68afb4039a247a1c20130a248Entities();

		[Authorize]
        public ActionResult Index(int userId = 0)
        {
			//Database
			ViewBag.Users = DBContext.Users;
			ViewBag.Questions = DBContext.Questions;
			ViewBag.SelectedUser = userId;

            return View();
        }

		[Authorize]
		[HttpPost]
		public ActionResult Index()
		{
			ViewBag.Users = DBContext.Users;
			ViewBag.Questions = DBContext.Questions;
			ViewBag.SelectedUser = 0;

			return View();
		}
    }
}
