using FluentValidation.Results;

namespace Messages
{
    public class ValueResponse<T> : HttpResponseBase where T : struct
    {
        public T Value { get; set; }

        public void SetSuccess(T value, HttpResultCode httpResultCode = HttpResultCode.Ok)
        {
            base.SetSuccess(httpResultCode);
            Value = value;
        }
        public void SetFailureWithValidation(ValidationResult message)
        {
            base.SetHttpFailureCode(message);
        }

        public void SetFailureWithValidation(ValidationResult message, T value)
        {
            base.SetHttpFailureCode(message);
            Value = value;
        }

    }
}
