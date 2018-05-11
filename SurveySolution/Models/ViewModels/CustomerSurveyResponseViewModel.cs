using SurveySolution.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveySolution.Models.ViewModels
{
    public class CustomerSurveyResponseViewModel
    {

        public int SurveyID { get; set; }
        [Display(Name = "Survey Title")]
        public string SurveyTitle { get; set; }
        [Display(Name = "Survey Description")]
        public string SurveyDesc { get; set; }

        public List<Question> Questions { get; set; }

        public List<Answer> Answers { get; set; }


        public int CustomerID { get; set; }

    }
}