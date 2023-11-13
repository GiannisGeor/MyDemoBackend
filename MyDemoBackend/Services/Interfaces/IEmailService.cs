using Messages;
using Models.Entities;
using Models.Entities.Auth;
using Models.Models.EmailNotification;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Generic Email Send
        /// </summary>
        /// <param name="allEmailInformation"></param>
        /// <returns></returns>
        Task<ValueResponse<bool>> GenericEmailSend(EmailNotificationRequiredInfo allEmailInformation);


        Task<EmailNotificationRequiredInfo> ConstructEmailNotificationRequiredInfoObject(
            EmailType emailType,
            AuthUser authUser = null, //For Confirmation Email
            string uri = null, //For Confirmation Email
            string encodedResetPasswordToken = null //For Forgot Password Email
            );
    }
}
