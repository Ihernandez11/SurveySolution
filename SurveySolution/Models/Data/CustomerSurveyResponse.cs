using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveySolution.Models.Data
{
    public class CustomerSurveyResponse
    {
        
        public int Id { get; set; }

        [Required, Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        [Required, Display(Name = "Survey ID")]
        public int SurveyId { get; set; }

        [Required, Display(Name = "Survey Name")]
        public string SurveyName { get; set; }

        [Required, Display(Name = "Question ID")]
        public int QuestionId { get; set; }

        [Required, Display(Name = "Question Name")]
        public string QuestionName { get; set; }

        [Required, Display(Name = "Answer ID")]
        public int AnswerId { get; set; }

        [Required, Display(Name = "Answer Value")]
        public string AnswerValue { get; set; }

        [Required, Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }


    }
}