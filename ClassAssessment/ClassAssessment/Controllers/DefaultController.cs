using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClassAssessment.Models;
using System.Diagnostics;
using System.Collections.Specialized;

namespace ClassAssessment.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/
		private ClassAssessment.Models.db92115aa68afb4039a247a1c20130a248Entities DBContext =
			new db92115aa68afb4039a247a1c20130a248Entities();
	
		public class NameShort
		{
			public int Id;
			public string Name, Surname;
		}

		public class QuestionShort
		{
			public int id;
			public string Text;
		}

		[Authorize]
        public ActionResult Index(int userId = 0)
        {
			//get id
			string name = HttpContext.User.Identity.Name.Split()[0];
			var currentUser = (from user in DBContext.Users
					  where user.Name == name
					  select user).First();

			ViewBag.Users = from user in DBContext.Users
							orderby user.Name
							select new NameShort
							{
								Id = user.Id,
								Name = user.Name,
								Surname = user.Surname
							};

            ViewBag.Rated = from assessment in currentUser.Assessments
                            join user in DBContext.Users
                            on assessment.To equals user.Id
                            select new NameShort
                            {
                                Id = user.Id,
                                Name = user.Name,
                                Surname = user.Surname
                            };
			
			ViewBag.Questions = (from x in DBContext.Questions
                                orderby x.id
								select new QuestionShort
								{
									id = x.id,
									Text = x.Text
								}).ToList();

            ViewBag.Answers = (from answer in DBContext.Answers
                              where answer.Assessments.From == currentUser.Id && answer.Assessments.To == userId
                              orderby answer.Questions.id
                              select answer.Value).ToList();

			ViewBag.SelectedUser = userId;

            return View();
        }

        private int InsertAssessment(Users currentUser, int assessedUserId, NameValueCollection answers)
        {
            var assessment = new Assessments()
            {
                From = currentUser.Id,
                To = assessedUserId,
            };

            DBContext.Assessments.Add(assessment);

            //questions
            for (int question = 1; true; question++)
            {
                var value = answers[question.ToString()];
                if (string.IsNullOrEmpty(value))
                    break;

                //they are ordered by id, so the first one is really the first one in the database
                var newAnswer = new Answers()
                {
                    Assessment = assessment.id,
                    Question = question,
                    Value = int.Parse(value)
                };

                DBContext.Answers.Add(newAnswer);
            }

            return DBContext.SaveChanges();
        }

        private int UpdateAssessment(Assessments assessment, Users currentUser, int assessedUserId, NameValueCollection answers)
        {
            for (int question = 1; true; question++)
            {
                var value = answers[question.ToString()];
                if (string.IsNullOrEmpty(value))
                    break;

                var ans = assessment.Answers.First((answer) => answer.Question == question);
                ans.Value = int.Parse(value);

            }

            return DBContext.SaveChanges();
        }

		[Authorize]
		[HttpPost]
		public ActionResult Index(int userId = 0, int shit = 1)
		{
			//get id
			string name = HttpContext.User.Identity.Name.Split()[0];
			var currentUser = (from x in DBContext.Users
					  where x.Name == name
					  select x).First();

            var previousAssessment = (from assessment in currentUser.Assessments
                                      where assessment.To == userId
                                      select assessment).FirstOrDefault();

            if (previousAssessment == null)
                InsertAssessment(currentUser, userId, Request.Form);
            else UpdateAssessment(previousAssessment, currentUser, userId, Request.Form);

            
            //int result = DBContext.SaveChanges();

			return Index(userId);
		}
    }
}
