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
            //create survey object to capture data returned in sqav model
            Survey survey = new Survey()
            {
                SurveyTitle = sqav.SurveyTitle,
                SurveyDescription = sqav.SurveyDesc
            };
            //Add the survey object to the surveys table
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

            //Return to the index page when complete
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
            //return bad request if id is null
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //create a survey object using the Find by the id parameter provided
            Survey survey = db.Surveys.Find(id);
            
            //create a list of questions for the questions in the db that map to the SurveyID
            List<Question> questions = db.Questions.Where(q => q.SurveyId == survey.Id).ToList();

            //If there is no survey found, return NotFound page
            if (survey == null)
            {
                return HttpNotFound();
            }

            //create the view model object to return to the view
            SurveyQuestionAnswerViewModel sqav = new SurveyQuestionAnswerViewModel
            {
                SurveyID = survey.Id,
                SurveyTitle = survey.SurveyTitle,
                SurveyDesc = survey.SurveyDescription,
                Questions = questions
            };
            //Create a current index to capture the number of question labels on the page
            ViewBag.QuestionCount = sqav.Questions.Count() - 1;

            
            
            //return the model to the view
            return View(sqav);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SurveyQuestionAnswerViewModel sqav)
        {
            //create a survey object from the model provided 
            Survey survey = new Survey()
            {
                Id = sqav.SurveyID,
                SurveyTitle = sqav.SurveyTitle,
                SurveyDescription = sqav.SurveyDesc
            };

            
                //if the data has changed, update it
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
            

            /*Loop through the list of questions in the viewmodel to add them to the question table if they are new*/
            for (int i=0; i < sqav.Questions.Count(); i++)
            {

                Question question = new Question
                {
                    Id = sqav.Questions[i].Id,
                    QuestionName = sqav.Questions[i].QuestionName,
                    QuestionType = "Free Response",
                    Required = sqav.Questions[i].Required,
                    SurveyId = sqav.SurveyID
                };

                //Update existing questions
                db.Entry(question).State = EntityState.Modified;

                
                //if it is a new question, question id will be 0 - add it to the DB
                if (question.Id == 0)
                {
                    db.Questions.Add(question);
                } 
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


        //GET: Surveys/Responses/SurveyId
        public ActionResult Responses(int? id)
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

            //Find the CustomerSurveyResponse data that maps to that survey ID
            List<CustomerSurveyResponse> csrList = db.CustomerSurveyResponses.Where(p => p.SurveyId == survey.Id).ToList();


            //CustomerSurveyResponse csr = CustomerSurveyResponse

            return View(csrList);
        }

        //GET Surveys/Customers/SurveyID
        //This view will show the list of customers after an admin clicks on Send from the survey page
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

            SurveyCustomerViewModel scv = new SurveyCustomerViewModel()
            {
                SurveyID = survey.Id,
                SurveyName = survey.SurveyTitle,
                Customers = db.Customers.ToList()
            };

            //This should return the Customer model with a list of customers
            return View(scv);
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
/*Questions:
 1. Why is my modelState invalid in edit page?
 2. Is there an easier way to update a list in the ViewModel?

     
     */
