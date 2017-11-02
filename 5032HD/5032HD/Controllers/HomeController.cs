using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using _5032HD.Models;

namespace _5032HD.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Angular()
        {
            return View();

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Documentation()
        {
            ViewBag.Message = "Task Documentation";

            return View();
        }

        public ActionResult OurHistory()
        {
            ViewBag.Message = "Our History";

            return View();
        }

        public ActionResult FAQ()
        {
            ViewBag.Message = "Frequently Asked Question";

            return View();
        }

        public ActionResult Contact()
        {
            var emailModel = new EmailFormModel();
            var usersDB = db.AspNetUsers.ToList();
            emailModel.Users = usersDB;
            emailModel.Emails = new List<string>();

            foreach (AspNetUser user in emailModel.Users)
            {
                emailModel.Emails.Add(user.Email);
            }
            ViewBag.EmailList = new MultiSelectList(emailModel.Emails);
            return View(emailModel);
        }
        [HttpPost]
        public async Task<ActionResult> Contact(EmailFormModel emailModel)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                //message.To.Add(new MailAddress("recipient@gmail.com"));  // replace with valid value 
                foreach (var item in emailModel.Emails)
                {
                    message.To.Add(new MailAddress(item));
                }
                //message.To.Add(new MailAddress("superyaoyite@gmail.com"));
                //message.To.Add(new MailAddress("yaoyite@yeah.net"));
                // replace with valid value 
                //message.From = new MailAddress("sender@outlook.com");  // replace with valid value
                message.From = new MailAddress("yyao0007@student.monash.edu");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, emailModel.FromName, emailModel.FromEmail, emailModel.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "yyao0007@student.monash.edu",  // replace with valid value
                        Password = ""  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    //smtp.host = "smtp-mail.outlook.com";
                    smtp.Host = "smtp.monash.edu.au";

                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587;
                    //smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(emailModel);
        }
    }
}