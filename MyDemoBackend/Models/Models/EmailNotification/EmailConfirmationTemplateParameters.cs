namespace Models.Models.EmailNotification
{
    public class EmailConfirmationTemplateParameters : BaseTemplateParameters
    {
        public EmailConfirmationSubjectTemplateParameters SubjectParams { get; set; }
        public EmailConfirmationBodyTemplateParameters BodyParams { get; set; }
    }

    public class EmailConfirmationSubjectTemplateParameters
    {
        public EmailConfirmationSubjectTemplateParameters(string userEmail, string emailConfirmationLink)
        {
            UserEmail = userEmail;
            EmailConfirmationLink = emailConfirmationLink;
        }

        public string UserEmail { get; set; }
        public string EmailConfirmationLink { get; set; }
    }
    public class EmailConfirmationBodyTemplateParameters
    {
        public EmailConfirmationBodyTemplateParameters(string userEmail, string emailConfirmationLink)
        {
            UserEmail = userEmail;
            EmailConfirmationLink = emailConfirmationLink;
        }

        public string UserEmail { get; set; }
        public string EmailConfirmationLink { get; set; }
    }
}
