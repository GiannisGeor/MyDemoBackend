using Models.Entities;

namespace Models.Models.EmailNotification
{
    public class EmailNotificationRequiredInfo
    {
        public EmailNotificationRequiredInfo(EmailType emailType)
        {
            EmailType = emailType;
        }
        public EmailType EmailType { get; set; }

        public EmailTemplate EmailTemplate { get; set; }

        public List<string> Recipients { get; set; }

        public EmailParameters EmailParameters { get; set; } = new EmailParameters();

    }

    public class EmailParameters
    {
        public Dictionary<string, string> SubjectTemplateParameters { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, string> BodyTemplateParameters { get; set; } = new Dictionary<string, string>();
    }
}
