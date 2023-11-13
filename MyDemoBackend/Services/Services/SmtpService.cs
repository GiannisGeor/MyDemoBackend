using Common.Settings;
using Microsoft.Extensions.Options;
using Serilog;
using Services.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Services.Services
{
    public class SmtpService : ISmtpService
    {
        private readonly EmailSettingsModel _options;

        public SmtpService(IOptions<EmailSettingsModel> options)
        {
            _options = options.Value;
        }

        public async Task SendAsync(string subject, string htmlContent, string textContent, List<string> to)
        {
            try
            {
                // Build message
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(_options.DisplayAddress, _options.DisplayName)
                };

                // Build receivers
                mail = BuildReceivers(mail, to);

                // Add text content
                mail.Subject = subject;
                mail.Body = textContent;

                // Add html content
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlContent);
                htmlView.ContentType = new ContentType(MediaTypeNames.Text.Html);
                mail.IsBodyHtml = true;
                mail.AlternateViews.Add(htmlView);

                // Build client and send
                SmtpClient client = new SmtpClient
                {
                    Port = Convert.ToInt32(_options.Port),
                    Host = _options.Server,
                    EnableSsl = _options.EnableSSL,
                    Credentials = new NetworkCredential(_options.Username, _options.Password),
                };

                await client.SendMailAsync(mail);
            }
            catch (Exception e)
            {
                Log.Error(e, $"Failed to send email.");
            }
        }

        public async Task SendAsync(string subject, string textContent, List<string> to)
        {
            try
            {
                // Build message
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(_options.DisplayAddress, _options.DisplayName)
                };

                // Build receivers
                mail = BuildReceivers(mail, to);

                // Add text content
                mail.Subject = subject;
                mail.Body = textContent;
                mail.IsBodyHtml = false;

                // Build client and send
                SmtpClient client = new SmtpClient
                {
                    Port = Convert.ToInt32(_options.Port),
                    Host = _options.Server,
                    EnableSsl = _options.EnableSSL,
                    Credentials = new NetworkCredential(_options.Username, _options.Password),
                };

                await client.SendMailAsync(mail);
            }
            catch (Exception e)
            {
                Log.Error(e, $"Failed to send email.");
            }
        }

        /// <summary>
        /// Method to add recipients to email
        /// </summary>
        /// <param name="mail">Mail message to be added to</param>
        /// <param name="to">List of recipients</param>
        /// <returns>The mail message with attached recipients</returns>
        private MailMessage BuildReceivers(MailMessage mail, List<string> to)
        {
            if (_options.UseOverrideEmail)
            {
                // Override per settings
                mail.To.Add(_options.OverrideAddress);
            }
            else
            {
                // Add each recipent
                to.ForEach(receiver =>
                {
                    mail.To.Add(receiver);
                });
            }

            // Add bcc if needed
            if (_options.UseBccAddress)
            {
                mail.Bcc.Add(_options.BccAddress);
            }

            return mail;
        }
    }
}
