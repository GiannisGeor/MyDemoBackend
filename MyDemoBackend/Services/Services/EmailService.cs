using Common.Configuration;
using Common.Extensions;
using Data.Interfaces;
using Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Models.Entities;
using Models.Entities.Auth;
using Models.Models.EmailNotification;
using Serilog;
using Services.Interfaces;
using Services.Interfaces.Translations;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ISmtpService _smtpService;
        private readonly IGenericRepository _genericRepository;
        private readonly ITranslationService _translationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmailService(
            IGenericRepository genericRepository,
            ISmtpService smtpService,
            IConfiguration configuration,
            ITranslationService translationService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _smtpService = smtpService;
            _genericRepository = genericRepository;
            _configuration = configuration;
            _translationService = translationService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ValueResponse<bool>> GenericEmailSend(EmailNotificationRequiredInfo allEmailInformation)
        {
            ValueResponse<bool> response = new ValueResponse<bool>();
            try
            {
                if (allEmailInformation.EmailTemplate == null)
                {
                    response.SetHttpFailureCode("Email template can not be null", HttpResultCode.NotFound);
                    return response;
                }
                var subject = await _translationService.GetTranslatedText(allEmailInformation.EmailTemplate.Subject);
                var textString = allEmailInformation.EmailTemplate.TextString;
                var bodyHtml = GetHtmlString(allEmailInformation.EmailType);

                // Maybe we could validate the givenParams to Match the Expected Params..

                //if (!ValidTemplateParameters(subject, allEmailInformation.EmailParameters.SubjectTemplateParameters)
                //    || !ValidTemplateParameters(bodyHtml, allEmailInformation.EmailParameters.BodyTemplateParameters)
                //    || !ValidTemplateParameters(textString, allEmailInformation.EmailParameters.BodyTemplateParameters)
                //    )
                //{
                //    Log.Error($@"Given Parameters does not match with the existing parameters in template file");
                //    response.SetHttpFailureCode($"Failed to send Email", HttpResultCode.BadRequest);
                //    return response;
                //}

                subject = ReplaceTemplateTextParametersWithValues(subject, allEmailInformation.EmailParameters.SubjectTemplateParameters);
                bodyHtml = ReplaceTemplateTextParametersWithValues(bodyHtml, allEmailInformation.EmailParameters.BodyTemplateParameters);
                textString = ReplaceTemplateTextParametersWithValues(textString, allEmailInformation.EmailParameters.BodyTemplateParameters);
                await _smtpService.SendAsync(subject, bodyHtml, textString, allEmailInformation.Recipients);

                response.SetSuccess(true);
                return response;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Error on email {Enum.GetName(typeof(EmailType), allEmailInformation.EmailType)} send");
                response.SetHttpFailureCode($"An error occured during email sending procedure", HttpResultCode.InternalServerError);
                return response;
            }
            return null;
        }

        public async Task<EmailNotificationRequiredInfo> ConstructEmailNotificationRequiredInfoObject(
            EmailType emailType,
            AuthUser authUser = null, //For Confirmation Email
            string uri = null, //For Confirmation Email
            string encodedResetPasswordToken = null //For Forgot Password Email
            )
        {
            // Here we would have 1 method for each different email type that will create the Email Object that contains all the information.
            switch (emailType)
            {
                case EmailType.ConfirmEmail:
                    return await GetConfirmationEmailRequiredInformation(emailType, authUser, uri);
                case EmailType.ForgotPassword:
                    return await GetForgotPasswordRequiredInformation(emailType, authUser, encodedResetPasswordToken);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Fetches the email template based on the emailType parameter
        /// </summary>
        /// <param name="emailType"></param>
        /// <returns></returns>
        private async Task<EmailTemplate> GetEmailTemplate(EmailType emailType)
        {
            var emailTemplate = new EmailTemplate();
            try
            {
                Expression<Func<EmailTemplate, bool>> predicate = (i => i.EmailType == emailType);
                emailTemplate = await _genericRepository.Single(predicate);
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed to load Email Templates");
            }

            return emailTemplate;
        }

        /// <summary>
        /// Fetches the html file for each htmlString according to emailType
        /// </summary>
        /// <param name="emailType"></param>
        /// <returns> The contents of the html file </returns>
        private string GetHtmlString(EmailType emailType)
        {
            try
            {
                // Grab locale from header

                var languageIdentifier = String.Empty;
                languageIdentifier = _translationService.GetLanguageIdentifierFromRequestHeader(_httpContextAccessor?.HttpContext?.Request);

                // Get the list of html email template
                var filesTemplate = new List<string>();
                if (languageIdentifier == "DE")
                {
                    filesTemplate.AddRange(Directory.EnumerateFiles(GlobalConstants.FolderHandling.EmailTemplatesDe).ToList());
                }
                else
                {
                    filesTemplate.AddRange(Directory.EnumerateFiles(GlobalConstants.FolderHandling.EmailTemplatesEn).ToList());
                }

                // The file namings are the same as the email template but with different naming convention
                var fileName = emailType.ToString().ToLower();

                // Pick the correct file from the list
                var htmlFile = filesTemplate.SingleOrDefault(x => x.ToLower().Contains(fileName));

                // Appends the file to the base directory path
                var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), htmlFile);

                // Reads and returns the contents
                var htmlContents = File.ReadAllText(path);

                return htmlContents;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Failed to read {emailType} html template");
                return "";
            }
        }

        /// <summary>
        /// Converts userEmail to emailList as it needed by the semtp service
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns> List of Emails </returns>
        private List<string> EmailList(string userEmail)
        {
            return new List<string>() { userEmail };
        }


        private async Task<EmailNotificationRequiredInfo> GetConfirmationEmailRequiredInformation(EmailType emailType, AuthUser authUser, string uri)
        {
            var completeLinkForEmailConfirmation = $"{_configuration["ApplicationBaseUrl"]}/email-confirmation?u={authUser.Id}&t={uri}";
            EmailConfirmationTemplateParameters tmpParams = new EmailConfirmationTemplateParameters()
            {
                SubjectParams = new EmailConfirmationSubjectTemplateParameters(authUser.Email, completeLinkForEmailConfirmation),
                BodyParams = new EmailConfirmationBodyTemplateParameters(authUser.Email, completeLinkForEmailConfirmation),
                EmailTemplate = await GetEmailTemplate(emailType)
            };

            EmailNotificationRequiredInfo candidate = new EmailNotificationRequiredInfo(EmailType.ConfirmEmail);
            candidate.EmailTemplate = tmpParams.EmailTemplate;
            candidate.Recipients = EmailList(authUser.Email);
            candidate.EmailParameters.SubjectTemplateParameters = tmpParams.SubjectParams.AsDictionary();
            candidate.EmailParameters.BodyTemplateParameters = tmpParams.BodyParams.AsDictionary();

            return candidate;
        }

        private async Task<EmailNotificationRequiredInfo> GetForgotPasswordRequiredInformation(EmailType emailType, AuthUser authUser, string encodedResetPasswordToken)
        {
            var completeLinkForForgotPassword = $"{_configuration["ApplicationBaseUrl"]}/reset-password?u={authUser.Id}&t={encodedResetPasswordToken}";
            ForgotPasswordTemplateParameters tmpParams = new ForgotPasswordTemplateParameters()
            {
                SubjectParams = new ForgotPasswordSubjectTemplateParameters(authUser.Email, completeLinkForForgotPassword),
                BodyParams = new ForgotPasswordBodyTemplateParameters(authUser.Email, completeLinkForForgotPassword),
                EmailTemplate = await GetEmailTemplate(emailType)
            };

            EmailNotificationRequiredInfo candidate = new EmailNotificationRequiredInfo(EmailType.ForgotPassword);
            candidate.EmailTemplate = tmpParams.EmailTemplate;
            candidate.Recipients = EmailList(authUser.Email);
            candidate.EmailParameters.SubjectTemplateParameters = tmpParams.SubjectParams.AsDictionary();
            candidate.EmailParameters.BodyTemplateParameters = tmpParams.BodyParams.AsDictionary();

            return candidate;
        }

        private string ReplaceTemplateTextParametersWithValues(string emailTemplateText, Dictionary<string, string> givenParameters)
        {
            foreach (var param in givenParameters)
            {
                emailTemplateText = emailTemplateText.Replace($@"{{#{param.Key}#}}", param.Value);
            }

            return emailTemplateText;
        }

        //private bool ValidTemplateParameters(string emailTemplateText, Dictionary<string, string> givenParameters)
        //{
        //    var pattern = new Regex(GlobalConstants.EmailConstants.EmailTemplateParametersRegularExpressionPattern);
        //    if (givenParameters.Count() != pattern.Matches(emailTemplateText).Count())
        //    {
        //        return false;
        //    }

        //    foreach (var param in givenParameters)
        //    {
        //        if (!emailTemplateText.Contains(param.Key, StringComparison.OrdinalIgnoreCase)) return false;
        //    }

        //    return true;
        //}

    }
}
