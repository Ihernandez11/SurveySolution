using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveySolution.Models.Data
{
    public class Survey
    {

        public int Id { get; set; }
        [Required, Display(Name ="Survey Name")]
        public string SurveyTitle { get; set; }

        [Display(Name = "Survey Description")]
        public string SurveyDescription { get; set; }

        public List<Question> QuestionIds { get; set; }

    }
}