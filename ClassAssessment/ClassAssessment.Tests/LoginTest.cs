using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ClassAssessment.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Principal;

namespace UnitTesting
{
	[TestClass]
	public class AuthenticationTest
	{
		private class MockHttpContext : HttpContextBase
		{
			public override IPrincipal User { get; set; }
		}

		private AuthenticationController GetController()
		{
			var controller = new AuthenticationController();
			var requestContext = new RequestContext(new MockHttpContext(), new RouteData());

			controller.ControllerContext = new ControllerContext(requestContext, controller);

			return controller;
		}

		private AuthenticationController controller;

		public AuthenticationTest()
		{
			controller = GetController();
		}

		[TestMethod]
		public void LoginTest()
		{
			var view = controller.Login("asdf", "asdf") as ViewResult;
			Assert.AreEqual(view.ViewName, "Index");

			view = controller.Logout() as ViewResult;
			Assert.AreEqual(view.ViewName, "Login");
		}
	}
}
