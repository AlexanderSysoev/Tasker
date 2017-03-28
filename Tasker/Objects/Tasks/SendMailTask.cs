using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tasker.Objects.Tasks.Settings;

namespace Tasker.Objects.Tasks
{
    public class SendMailTask : ITask
    {
        private SendMailSettings _settings;

        public SendMailTask(string settingsJson)
        {
            _settings = JsonConvert.DeserializeObject<SendMailSettings>(settingsJson);
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            var mail = new MailMessage {From = new MailAddress(_settings.Message.From)};
            mail.To.Add(new MailAddress(_settings.Message.To));
            mail.Subject = _settings.Message.Subject;
            mail.Body = _settings.Message.Body;

            if (!cancellationToken.IsCancellationRequested)
            {
                using (var client = new SmtpClient
                {
                    Host = _settings.SmtpSettings.SmtpServer,
                    Port = _settings.SmtpSettings.Port,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_settings.SmtpSettings.Login, _settings.SmtpSettings.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                })
                {
                    await client.SendMailAsync(mail);
                }
            }
        }
    }
}
