using FluentValidation.Results;

namespace Messages
{
    public abstract class ResponseBase
    {
        public string ErrorDetails { get; set; }

        public bool Success { get; set; }

        public ValidationResult ValidationResult { get; private set; }

        protected ResponseBase()
        {
        }

        public virtual void SetSuccess()
        {
            Success = true;
        }

        protected void SetFailure(string errorDetais)
        {
            Success = false;
            ErrorDetails = errorDetais;
        }

        protected void SetFailure(ValidationResult validation)
        {
            ValidationResult = validation;
            Success = false;
        }

        #region methods not used yet

        /*  public List<string> ErrorDetails { get; set; }

          public virtual void AddError(string error)
          {
              ErrorDetails.Add(error);
          }

          public virtual void SetException(Exception ex)
          {
              while (ex != null)
              {
                  AddError(ex.Message);
                  ex = ex.InnerException;
              }
              Success = false;
          }*/

        #endregion

    }
}