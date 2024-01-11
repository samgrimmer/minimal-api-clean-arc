using Application.Models;
using FluentValidation;

namespace Application.Service
{
    public class ValidatorService<T> : IValidatorService<T>
    {
        private readonly IValidator<T> _validator;

        public ValidatorService(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async Task<List<Error>> Validate(T dto)
        {
            List<Error> errors = [];

            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                errors.AddRange(validationResult.Errors.Select(e => new Error
                {
                    PropertyName = e.PropertyName,
                    ErrorMessage = e.ErrorMessage
                }));
            }

            return errors;
        }
    }
}
