using FluentValidation.Results;
using System.Collections.Generic;

namespace Messages
{
    public class ListResponse<T> : HttpResponseBase where T : class
    {
        public List<T> Items { get; set; } = new List<T>();

        public new List<ValidationResult> ValidationResult { get; set; } = new List<ValidationResult>();

        public void SetSuccess(List<T> items, HttpResultCode httpResultCode = HttpResultCode.Ok)
        {
            base.SetSuccess(httpResultCode);
            Items = items;
        }

        public void SetFailedValidationList(List<ValidationResult> validationList)
        {
            base.SetHttpFailureCode();
            ValidationResult = validationList;
        }

    }
}
