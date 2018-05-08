using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveySolution.Models;
using SurveySolution.Models.Data;
using SurveySolution.Models.ViewModels;

namespace SurveySolution.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SurveysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Surveys
        public ActionResult Index()
        {
            return View(db.Surveys.ToList());
        }

        // GET: Surveys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // GET: Surveys/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SurveyQuestionAnswerViewModel sqav)
        {
            

            Survey survey = new Survey()
            {
                SurveyTitle = sqav.SurveyTitle,
                SurveyDescription = sqav.SurveyDesc
            };

            db.Surveys.Add(survey);
            db.SaveChanges();
            
            /*Loop through the list of questions in the viewmodel to add them to the question table*/
            foreach(var q in sqav.Questions)
            {
                Question question = new Question
                {
                    QuestionName = q.QuestionName,
                    QuestionType = "Free Response",
                    Required = q.Required,
                    SurveyId = survey.Id
                };

                db.Questions.Add(question);
            }

            db.SaveChanges();

            //List<Answer> answers = new List<Answer>();

            return RedirectToAction("Index");
            
        }

        /* public ActionResult Create([Bind(Include = "Id,SurveyTitle,SurveyDescription")] Survey survey)
        {
            //Need to create a viewModel with Survey, Question, and Answer data so that I can create db records for Survey, Question, and Answer
            if (ModelState.IsValid)
            {
                db.Surveys.Add(survey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(survey);
        }*/

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Survey survey = db.Surveys.Find(id);
            List<Question> questions = db.Questions.Where(q => q.SurveyId == survey.Id).ToList();

            SurveyQuestionAnswerViewModel sqav = new SurveyQuestionAnswerViewModel
            {
                SurveyID = survey.Id,
                SurveyTitle = survey.SurveyTitle,
                SurveyDesc = survey.SurveyDescription,
                Questions = questions
            };

            ViewBag.QuestionCount = sqav.Questions.Count() - 1;

            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(sqav);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SurveyQuestionAnswerViewModel sqav)
        {
            Survey survey = new Survey()
            {
                SurveyTitle = sqav.SurveyTitle,
                SurveyDescription = sqav.SurveyDesc
            };

            
            db.SaveChanges();

            /*Loop through the list of questions in the viewmodel to add them to the question table*/
            foreach (var q in sqav.Questions)
            {
                Question question = new Question
                {
                    QuestionName = q.QuestionName,
                    QuestionType = "Free Response",
                    Required = q.Required,
                    SurveyId = survey.Id
                };
                //if the question doesn't exist in db.questions, add it
                

            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        /*public ActionResult Edit([Bind(Include = "Id,SurveyTitle,SurveyDescription")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(survey);
        }*/

        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Survey survey = db.Surveys.Find(id);
            List<Question> questions = db.Questions.Where(q => q.SurveyId == survey.Id).ToList();
            db.Surveys.Remove(survey);
            db.Questions.RemoveRange(questions);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Response(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Survey survey = db.Surveys.Find(id);

            if(survey == null)
            {
                return HttpNotFound();
            }
            //I should return the CustomerSurveyResponse model
            return View();
        }

        public ActionResult Customers(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Survey survey = db.Surveys.Find(id);

            if (survey == null)
            {
                return HttpNotFound();
            }
            //This should return the Customer model with a list of customers
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
