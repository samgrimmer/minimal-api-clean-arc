using Application.Models;

namespace Application.Service
{
    public interface IValidatorService<T>
    {
        Task<List<Error>> Validate(T dto);
    }
}