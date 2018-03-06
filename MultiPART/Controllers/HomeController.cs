using MultiPART.Authorization;
using System;
using System.Net.Mail;
using System.Web.Mvc;

namespace MultiPART.Controllers
{
    [Authorize]
    [Restrict("Disabled")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles="Administrator")]
        //public ActionResult ContactUs()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ContactUs(ContactForm)
        //{



        //    return View();
        //}

        #region Helpers

        private void sendemail(string user, string message)
        {

            MailMessage mail = new MailMessage();
            
            mail.To.Add(new MailAddress(user));
            mail.From = new MailAddress("MultiPART.team@ed.ac.uk");
            mail.Subject = "Message from Multi-PART";

            mail.Body = message;

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();

            // Attempt to send the email
            try
            {
                smtp.Send(mail);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Issue sending email: " + e.Message);
            }
        }


        #endregion
    }
}
