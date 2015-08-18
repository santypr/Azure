using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace WebJobs.SendEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            var sgUsername = "YOUR_SENDGRID_USER"; // Take it from the sendgrid configuration at Azure Portal
            var sgPassword = "YOUR_SENDGRID_SERVICE"; // The password you configured when creating sendgrid service
            var message = CreateEmail();
            AddAttachment(message);
            SendEmail(sgUsername, sgPassword, message);
        }

        private static MailMessage CreateEmail()
        {
            // Create the email object first, then add the properties.
            var message = new MailMessage();

            // Add the message properties.
            message.From = new MailAddress("santiagoporras@santiagoporras.com");

            // Add multiple addresses to the To field.
            var recipients = @"Santiago Porras Rodríguez <demo1@gmail.com>,
                               Santiago Porras Rodríguez <demo2@outlook.com>,
                               Usuario Demo 1 <demo1@outlook.com>,
                               Usuario Demo 2 <demo2@outlook.com>";
            

            message.To.Add(recipients);

            message.Subject = "Testing the SendGrid Library";

            //Add the HTML and Text bodies
            var html = "<h1>¡santiagoporras.com próximamente!</h1><p>En breve estará disponible mi sitio web. No dudes en visitarlo</p>";
            var text = "¡santiagoporras.com próximamente!\n En breve estará disponible mi sitio web. No dudes en visitarlo";

            message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Html));

            return message;
        }

        private static void SendEmail (string sgUsername, string sgPassword, MailMessage message)
        {
            // Set credentials
            var credentials = new NetworkCredential(sgUsername, sgPassword);

            // Create SMTP cliente
            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            smtpClient.Credentials = credentials;

            // Send email
            smtpClient.Send(message);
        }

        private static void AddAttachment(MailMessage message)
        {
            //message.AddAttachment(System.IO.Path.GetFullPath("santiago-porras-logo.png"));
        }
    }
}
