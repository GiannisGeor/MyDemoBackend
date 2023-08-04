using FluentValidation.Results;

namespace Messages
{
    public class HttpResponseBase : ResponseBase
    {
        public HttpResultCode HttpResultCode { get; set; }

        public void SetSuccess(HttpResultCode httpResultCode = HttpResultCode.Ok)
        {
            base.SetSuccess();
            HttpResultCode = httpResultCode;
        }

        public void SetHttpFailureCode(string errorDetails, HttpResultCode httpResultCode)
        {
            base.SetFailure(errorDetails);
            HttpResultCode = httpResultCode;
        }

        public void SetHttpFailureCode(ValidationResult validattion)
        {
            base.SetFailure(validattion);
            HttpResultCode = HttpResultCode.Ok;
        }

        public void SetHttpFailureCode()
        {
            HttpResultCode = HttpResultCode.Ok;
        }

    }
}
