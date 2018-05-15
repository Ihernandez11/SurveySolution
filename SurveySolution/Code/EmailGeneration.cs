using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SurveySolution.Code
{
    public class EmailGeneration
    {
        
        public void SendEmail(MailAddress toEmail, string surveyURL)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 465;

            MailAddress from = new MailAddress("Israel.hernandez11@gmail.com", "Izzy");

            MailMessage message = new MailMessage(from, toEmail);

            message.Body = "You have been invited to take the Survey below.";
            message.Body += Environment.NewLine + surveyURL;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            message.Subject = "Take this survey!";
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            client.Send(message);

            message.Dispose();

        }
        

    }
}