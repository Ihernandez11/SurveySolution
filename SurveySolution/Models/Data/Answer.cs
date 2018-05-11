using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurveySolution.Models.Data
{
    public class Answer
    {

        public int Id { get; set; }
        [Display(Name = "Answer Value")]
        public string AnswerValue { get; set; }

        [Required]
        public int QuestionId { get; set; }

    }
}