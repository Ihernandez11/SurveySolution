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

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int SurveyId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int AnswerId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }


    }
}