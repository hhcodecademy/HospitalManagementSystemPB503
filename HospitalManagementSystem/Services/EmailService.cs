using System.Net.Mail;
using System.Net;
using HospitalManagementSystem.Services.Interfaces;

namespace HospitalManagementSystem.Services
{
    public class EmailService : IEmailService
    {

        public async Task SendEmailAsync(string emailToAddress, string subject, string body)
        {
            var emailFromAddress = "huseyn.hasanli@code.edu.az";


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(emailFromAddress);
            mail.To.Add(emailToAddress);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(emailFromAddress, "wavc jtmj pzpn znah"),
                EnableSsl = true,
            };
            client.SendAsync(mail, null);
            await Task.CompletedTask; 


        }
    }
    
}
