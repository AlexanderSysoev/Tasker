namespace Tasker.Objects.Tasks.Settings
{
    public class SendMailSettings
    {
        public MessageInfo Message { get; set; }

        public SmtpSettings SmtpSettings { get; set; }
    }

    public class MessageInfo
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }

    public class SmtpSettings
    {
        public string SmtpServer { get; set; }

        public int Port { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
