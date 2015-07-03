using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Threading.Tasks;
using Edstart.Models;
namespace Edstart.Services
{
    //public interface IEmail
    //{
    //    public string FromEmail;

    //    public string ToEmail;

    //    public string Body;
    //    void Send();
    //}

    public class Email_Service
    {
        private string ToEmail;

        private string Body;

        private MailMessage Email;

        private MailAddress FromEmail = new MailAddress("project.edstart@gmail.com", "Edstart"); 

        private void ConfigEmailMessage(string Subject,string ToEmail="",string Body="")
        {
            Email = new MailMessage();
            Email.To.Add(new MailAddress(ToEmail));
            Email.From = FromEmail;
            Email.Subject = Subject;
            Email.Body = Body;
            Email.IsBodyHtml = true;
        }
        private void ConfigEmailMessage(string Subject, List<string> ToEmails , string Body = "")
        {
            Email = new MailMessage();
            foreach( var ToEmail in ToEmails){
               Email.To.Add(new MailAddress(ToEmail));
            }
            
            Email.From = FromEmail;
            Email.Subject = Subject;
            Email.Body = Body;
            Email.IsBodyHtml = true;
        }

        private bool Send()
        {
            try
            {
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "project.edstart",
                        Password = "Edstart12345678"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(Email);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EmailInteresting(string email)
        {
            string Body = "Email : " + email;
            ConfigEmailMessage("Edstart - Email Interesting", Config.EdstartMail, Body);
            return Send();
        }

        public bool BorrowerRegister(string email, string code)
        {
            string url = "http://" + Config.HostName + "/Parent/EmailApprove?email=" + email + "&code=" + code;
            string Body = "<a href='" + url + "'>Click here for approve account</a>";
            ConfigEmailMessage("Edstart - Email Approve", email, Body);
            return Send();
        }
        public bool SchoolRegister(string email)
        {
            string Body = "Welcome you";
            ConfigEmailMessage("Edstart - Congratulations", email, Body);
            return Send();
        }
        public bool InvestorRegister(string email)
        {
            string Body = "Welcome you";
            ConfigEmailMessage("Edstart - Congratulations", email, Body);
            return Send();
        }

        public bool Notification_Register(string email,eRole kind)
        {
            string Body = "Email : " + email;
            ConfigEmailMessage("Edstart - A new " + kind.ToString() + " had registed", Config.EdstartMail , Body);
            return Send();
        }

        public bool Notification_InvesmentSuccess(string ParentEmail , List<string> InvestorEmails)
        {
            // send to Parent
            string Body = "Your loan founded enough money .";
            ConfigEmailMessage("Edstart - Congratulations", ParentEmail, Body);
            Send();
            // send to Edstart
            Body = ParentEmail + " founded enough money .";
            ConfigEmailMessage("Edstart - A investment success", Config.EdstartMail, Body);
            Send();
            // send to Investors
            Body = "You invest to " + ParentEmail + " successfully";
            ConfigEmailMessage("Edstart - Investment Success", InvestorEmails, Body);
            return Send();
        }


    }


    public class EdStartEmail
    {
        //[Required, Display(Name = "Your name"), EmailAddress]
        //public string FromEmail{get;set;}

        //[Required, Display(Name = "Your name"), EmailAddress]
        //public string ToEmail{get;set;}

        [Required, Display(Name = "Email"), EmailAddress]
        public string Message { get; set; }

        //public bool Send()
        //{
        //    try
        //    {
        //        var message = new MailMessage();
        //        message.To.Add(new MailAddress("info@edstart.com.au"));  // info@edstart.com.au
        //        message.From = new MailAddress("project.edstart@gmail.com", "Project Edstart");  // 
        //        message.Subject = "Email Registration";
        //        message.Body = String.Format("<p>Email : {0} </p>", Message);
        //        message.IsBodyHtml = true;

        //        var smtp = new SmtpClient("smtp.gmail.com", 587);
        //        var credential = new NetworkCredential
        //           {
        //               UserName = "project.edstart@gmail.com",
        //               Password = "Edstart12345678"
        //           };
        //        smtp.Credentials = credential;
        //        smtp.EnableSsl = true;
        //        smtp.Send(message);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //}
    }
}