using SurveySolution.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveySolution.Models.ViewModels
{
    public class SurveyQuestionAnswerViewModel
    {
        public int SurveyID { get; set; }
        [Display(Name = "Survey Title")]
        public string SurveyTitle { get; set; }
        [Display(Name = "Survey Description")]
        public string SurveyDesc { get; set; }

        public List<Question> Questions { get; set; }


        /*public int QuestionID { get; set; }
        [Display(Name = "Question Text")]
        public string QuestionName { get; set; }
        [Display(Name = "Survey Type")]
        public string QuestionType { get; set; }
        [Display(Name = "Required?")]
        public bool Required { get; set; }*/



        public List<Answer> Answers { get; set; }




    }
}