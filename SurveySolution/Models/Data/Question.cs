using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveySolution.Models.Data
{
    public class Question
    {

        public int Id { get; set; }

        [Required, Display(Name = "Question")]
        public string QuestionName { get; set; }

        [Required, Display(Name = "Question Type")]
        public string QuestionType { get; set; }

        [Required]
        public bool Required { get; set; }

        [Required]
        public int SurveyId { get; set; }

        public List<Answer> AnswerIds { get; set; }

    }
}