using Application.Enumeration;

namespace Application.Models
{
    public class ApplicationResult<T>
    {
        public ApplicationResultStatus Status { get; set; }
        public T Data { get; set; }
        public List<Error>? ValidationErrors { get; set; }

        private ApplicationResult(ApplicationResultStatus status, T data, List<Error>? validationErrors = null)
        {
            Status = status;
            Data = data;
            ValidationErrors = validationErrors;
        }

        public static ApplicationResult<T> Ok(T result)
        {
            return new ApplicationResult<T>(ApplicationResultStatus.Ok, result);
        }

        public static ApplicationResult<T> BadRequest(List<Error> errors)
        {
            return new ApplicationResult<T>(ApplicationResultStatus.BadRequest, default(T), errors);
        }

        public static ApplicationResult<T> NotFound()
        {
            return new ApplicationResult<T>(ApplicationResultStatus.NotFound, default(T));
        }
    }
}
