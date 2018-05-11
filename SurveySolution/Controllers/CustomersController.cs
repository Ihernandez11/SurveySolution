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
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        //Get: Customers/Surveys/ID
        public ActionResult Survey(int? id, int? CustomerId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (CustomerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            //create a survey object using the Find by the id parameter provided
            Survey survey = db.Surveys.Find(id);
            //create a list of questions for the questions in the db that map to the SurveyID
            List<Question> questions = db.Questions.Where(q => q.SurveyId == survey.Id).ToList();

            Customer customer = db.Customers.Find(CustomerId);

            //if no survey found, return not found page
            if (survey == null)
            {
                return HttpNotFound();
            }

            if (customer == null)
            {
                return HttpNotFound();
            }

            //create the view model object to return to the view
            CustomerSurveyResponseViewModel csrv = new CustomerSurveyResponseViewModel()
            {
                SurveyID = survey.Id,
                SurveyTitle = survey.SurveyTitle,
                SurveyDesc = survey.SurveyDescription,
                Questions = questions,
                CustomerID = customer.Id
            };

            return View(csrv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Survey(CustomerSurveyResponseViewModel csrv)
        {


            //create a survey object from the model provided 
            Survey survey = new Survey()
            {
                Id = csrv.SurveyID,
                SurveyTitle = csrv.SurveyTitle,
                SurveyDescription = csrv.SurveyDesc
            };


            /*Loop through the list of questions in the viewmodel to add the corresponding answers to the answer table*/
            for (int i = 0; i < csrv.Questions.Count(); i++)
            {
                

                //Capture the answer value
                Answer answer = new Answer()
                {
                    AnswerValue = csrv.Answers[i].AnswerValue,
                    QuestionId = csrv.Questions[i].Id
                };
                //If AnswerValue is null, don't add it
                if (answer.AnswerValue == null && db.Questions.Find(answer.QuestionId).Required == true)
                {
                    ModelState.AddModelError(string.Empty, "Please answer all required questions!");

                    return View("Survey",csrv);
                }
                else if(answer.AnswerValue != null)
                {
                    db.Answers.Add(answer);
                    db.SaveChanges();
                }

                //capture the SurveyResponse value
                CustomerSurveyResponse csr = new CustomerSurveyResponse()
                {
                    SurveyId = csrv.SurveyID,
                    QuestionId = csrv.Questions[i].Id,
                    AnswerId = answer.Id,
                    CustomerId = csrv.CustomerID,
                    CreateDate = DateTime.Now
                };
                
                //If AnswerValue is null, don't add it
                if(answer.AnswerValue != null)
                {
                    db.CustomerSurveyResponses.Add(csr);
                }
                
                
            }

            db.SaveChanges();


            return View("Success");
        }

        // GET: Customers/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,FirstName,LastName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,FirstName,LastName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
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

