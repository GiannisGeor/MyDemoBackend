namespace Models.Models.EmailNotification
{
    public class ForgotPasswordTemplateParameters : BaseTemplateParameters
    {
        public ForgotPasswordSubjectTemplateParameters SubjectParams { get; set; }
        public ForgotPasswordBodyTemplateParameters BodyParams { get; set; }
    }

    public class ForgotPasswordSubjectTemplateParameters
    {
        public ForgotPasswordSubjectTemplateParameters(string userEmail, string passwordResetLink)
        {
            UserEmail = userEmail;
            PasswordResetLink = passwordResetLink;
        }

        public string UserEmail { get; set; }
        public string PasswordResetLink { get; set; }
    }
    public class ForgotPasswordBodyTemplateParameters
    {
        public ForgotPasswordBodyTemplateParameters(string userEmail, string passwordResetLink)
        {
            UserEmail = userEmail;
            PasswordResetLink = passwordResetLink;
        }

        public string UserEmail { get; set; }
        public string PasswordResetLink { get; set; }
    }
}
