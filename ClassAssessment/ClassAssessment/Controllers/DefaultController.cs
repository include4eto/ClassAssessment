using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClassAssessment.Models;
using System.Diagnostics;
using System.Collections.Specialized;
using ClassAssessment.Model;
using ClassAssessment.Helpers;

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
			//get id
			string name = HttpContext.User.Identity.Name.Split()[0];
			var currentUser = (from user in DBContext.Users
					  where user.Name == name
					  select user).First();

			ViewBag.Users = from user in DBContext.Users
							orderby user.Name
                            where user.Roles != "Admin"
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

                int valueInt = 1;
                int.TryParse(value, out valueInt);

                //they are ordered by id, so the first one is really the first one in the database
                var newAnswer = new Answers()
                {
                    Assessment = assessment.id,
                    Question = question,
                    Value = Statistics.Clamp(1, 5, valueInt)
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
		public ActionResult Index(int userId = 2, int shit = 1)
		{
            //validation
            var role = (from user in DBContext.Users
                       where user.Id == userId
                       select user.Roles).First();
            if (role == "Admin")
                return View(userId);

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

        [Authorize(Users="Ирина Шаркова")]
        public ActionResult Summary(int userId = 2)
        {
            var targetUser = from user in DBContext.Users
                             where user.Id == userId
                             select user;

            List<AnsweredQuestion> answers = new List<AnsweredQuestion>();
            foreach (var question in DBContext.Questions)
            {
                var answer = (from ans in question.Answers
                              where ans.Assessments.To == userId
                              orderby ans.Value
                              select new AnswerShort()
                              {
                                  User = ans.Assessments.Users.Name + " " + ans.Assessments.Users.Surname,
                                  Value = ans.Value
                              }).ToList();

                double? average = 0, median = 0;
                double mode = 0;

                if (answer.Count > 0)
                {
                    average = answer.Average(ans => ans.Value.Value);
                    median = answer.Count % 2 == 0 ? (answer[answer.Count / 2].Value + answer[answer.Count / 2 - 1].Value) / 2.0
                        : answer[(int)Math.Floor((double)answer.Count / 2)].Value;
                    mode = Statistics.GetModeFromAnswers(answer, 6);
                }

                answers.Add(new AnsweredQuestion()
                {
                    Answers = answer,
                    Text = question.Text,
                    average = average.HasValue ? average.Value : 0,
                    median = median.HasValue ? median.Value : 0,
                    mode = mode,
                    Id = question.id
                });
            }

            ViewBag.User = targetUser.First();
            ViewBag.Questions = answers;
            ViewBag.Users = from user in DBContext.Users
                            orderby user.Name
                            where user.Roles != "Admin"
                            select new NameShort
                            {
                                Id = user.Id,
                                Name = user.Name,
                                Surname = user.Surname
                            };
            ViewBag.selectedUser = userId;

            return View();
        }

        [Authorize]
        public ActionResult Print()
        {
            return View();
        }
    }
}
