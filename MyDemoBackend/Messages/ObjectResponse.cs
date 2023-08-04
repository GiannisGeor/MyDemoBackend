using FluentValidation.Results;

namespace Messages
{
    public class ObjectResponse<T> : HttpResponseBase where T : class
    {
        public T Item { get; set; }

        public ObjectResponse()
        {

        }

        public ObjectResponse(T item)
        {
            Item = item;
            HttpResultCode = HttpResultCode.Ok;
        }

        public void SetSuccess(T item, HttpResultCode httpResultCode = HttpResultCode.Ok)
        {
            base.SetSuccess(httpResultCode);
            Item = item;
        }

        public void SetFailureWithValidation(ValidationResult message)
        {
            base.SetHttpFailureCode(message);
        }

        public void SetFailureWithValidation(ValidationResult message, T item)
        {
            base.SetHttpFailureCode(message);
            Item = item;
        }

    }
}
