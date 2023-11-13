using Common.Configuration;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.RegularExpressions;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Common.Methods
{
    public static class GlobalMethods
    {
        public static int GetAppropriateCustomerId(ClaimsPrincipal user)
        {     
            // Otherwise it means that the call was from a controller with User Role authorization Atribute, we get the customerId from the User's Claims and return it if exists. If it dies not exist we return 0.
            int customerId;
            var successfullyParsed = int.TryParse(user.FindFirst(GlobalConstants.Authentication.CustomClaims.CustomerId).Value, out customerId);
            return successfullyParsed ? customerId : 0;
        }

        public static string GetAppropriateCustomerEmail(ClaimsPrincipal user)
        {
            // Otherwise it means that the call was from a controller with User Role authorization Atribute, we get the customerId from the User's Claims and return it if exists. If it dies not exist we return 0.
            string customerEmail = user.FindFirst(ClaimTypes.Email).Value;
            return customerEmail;
        }

        /// <summary>
        /// Checks if email is valid
        /// </summary>
        /// <param name="email"></param>
        /// <param name="useRegEx">TRUE to use the HTML5 living standard e-mail validation RegEx, FALSE to use the built-in validator provided by .NET (default: FALSE)</param>
        /// <param name="requireDotInDomainName">TRUE to only validate e-mail addresses containing a dot in the domain name segment, FALSE to allow "dot-less" domains (default: FALSE)</param>
        /// <returns>ValidationResult with error list if found</returns>
        public static ValidationResult ValidateEmail(string email, bool useRegEx = false, bool requireDotInDomainName = false)
        {
            ValidationResult validationResult = new ValidationResult();

            if (!IsValidEmailAddress(email, useRegEx, requireDotInDomainName))
            {
                validationResult.Errors.Add(new ValidationFailure("Email", $"Email is not Valid"));
            }

            return validationResult;
        }

        /// <summary>
        /// Checks if the given e-mail is valid using various techniques
        /// </summary>
        /// <param name="email">The e-mail address to check / validate</param>
        /// <param name="useRegEx">TRUE to use the HTML5 living standard e-mail validation RegEx, FALSE to use the built-in validator provided by .NET (default: FALSE)</param>
        /// <param name="requireDotInDomainName">TRUE to only validate e-mail addresses containing a dot in the domain name segment(not ending with dot), FALSE to allow "dot-less" domains (default: FALSE)</param>
        /// <returns>TRUE if the e-mail address is valid, FALSE otherwise.</returns>
        private static bool IsValidEmailAddress(string email, bool useRegEx = false, bool requireDotInDomainName = false)
        {
            string EmailValidation_Regex = @"^[a-zA-Z0-9.!#$%&'*+\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

            Regex EmailValidation_Regex_Compiled = new Regex(EmailValidation_Regex, RegexOptions.IgnoreCase);

            var isValid = useRegEx
                    // see RegEx comments
                    ? email is not null && EmailValidation_Regex_Compiled.IsMatch(email)
                    : new EmailAddressAttribute().IsValid(email);

            if (isValid && requireDotInDomainName)
            {
                var arr = email.Split('@', StringSplitOptions.RemoveEmptyEntries);
                isValid = arr.Length == 2 && arr[1].Contains(".") && !arr[1].EndsWith(".");
            }
            return isValid;
        }

    }
}
