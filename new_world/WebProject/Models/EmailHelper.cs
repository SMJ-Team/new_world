using System.Net.Mail;

namespace WebProject.Models
{
    public class EmailHelper
    {
        public bool SendEmail(string userEmail, string confirmationLink)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("samuraikeksik@gmail.com");
            mailMessage.To.Add(userEmail);

            mailMessage.Subject = "Confirm your Email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("samuraikeksik@gmail.com", "Pas");
            client.Host = "smtpout.secureserver.net";
            client.Port = 80;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                //log
            }
            return false;
        }
    }
}
