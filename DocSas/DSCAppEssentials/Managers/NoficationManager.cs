using System;
using System.Collections.Generic;
using System.Text;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net.Mail;
using DSCAppEssentials.Helpers;
using System.Text.RegularExpressions;

namespace DSCAppEssentials.Managers
{
   
    public class NoficationManager
    {
        /// <summary>
        /// send email as an asynchronous operation.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Task.</returns>
        public static async Task SendEmailAsync(DSCEmail email)
        {
            var apiKey = email.EmailApiKey;

            var client = new SendGridClient(apiKey);

           var fromEmail = new EmailAddress(email.FromEmail.Item1, email.FromEmail.Item2);

            var toEmail = new List<EmailAddress>();

            foreach (var item in email.ToEmail)
            {
                toEmail.Add(new EmailAddress(item));
            }

            var message = MailHelper.CreateSingleEmailToMultipleRecipients(fromEmail, toEmail, email.Subject,email.PlainTextContent,email.HTMLContent);

            await client.SendEmailAsync(message);

        }

    }
}
