using SurveySolution.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurveySolution.Models.ViewModels
{
    public class SurveyCustomerViewModel
    {

        public int SurveyID { get; set; }
        public string SurveyName { get; set; }

        public List<Customer> Customers { get; set; }

        public List<bool> isSelected { get; set; }


    }
}