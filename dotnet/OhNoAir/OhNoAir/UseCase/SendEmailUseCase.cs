using OhNoAir.Models;
using System.Net;
using System.Net.Mail;

namespace OhNoAir.UseCase
{
    public class SendEmailUseCase
    {
        public bool SendEmail(List<string> emails, Tracking tracking)
        {
            bool success = true;
            try
            {
                if(emails == null || emails.Count == 0 || tracking == null) return false;
                foreach(var email in emails)
                {
                    var fromAddress = new MailAddress("cs380group5@gmail.com", "CS 380 Group 5");
                    var toAddress = new MailAddress(email, "Customer");
                    const string fromPassword = "ComSci380";
                    const string subject = "Flight ticket confirmation";
                    string body = $"TrackingID: {tracking.TrackingID}. To track your flight go to https://testcompsci380.azurewebsites.net/Tracking";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
                
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }
    }
}
