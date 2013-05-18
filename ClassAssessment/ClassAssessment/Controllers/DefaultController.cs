using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassAssessment.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/

		[Authorize]
        public ActionResult Index()
        {
			//Database

            return View("~/Views/Index.cshtml");
        }

    }
}
