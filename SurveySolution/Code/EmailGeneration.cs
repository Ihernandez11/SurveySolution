using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Configuration;

namespace SurveySolution.Code
{
    public class EmailGeneration
    {
        
        public static void SendEmail(string toEmail, string surveyURL)
        {
                string apiKey = ConfigurationManager.AppSettings["SendgridAPIKey"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(toEmail, "Israel Hernandez");
                var subject = "Please take this survey!";
                var to = new EmailAddress(toEmail);
                var plainTextContent = "You have been invited to take the survey below:";
                var htmlContent = "<strong> localhost:/" + surveyURL + " </strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = client.SendEmailAsync(msg);
            
        }



            
        }
    }


