using Application.Models;
using Application.Service;
using Infrastructure.Data;
using MediatR;

namespace Application.Features.User.Command
{
    public record CreateUserCommand : IRequest<ApplicationResult<Domain.Entity.User>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class CreateUserCommandHander : IRequestHandler<CreateUserCommand, ApplicationResult<Domain.Entity.User>>
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IValidatorService<CreateUserCommand> _validator;

        public CreateUserCommandHander(DatabaseContext databaseContext, IValidatorService<CreateUserCommand> validator)
        {
            _databaseContext = databaseContext;
            _validator = validator;
        }

        public async Task<ApplicationResult<Domain.Entity.User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validationErrors = await _validator.Validate(request);

            if (validationErrors.Any())
            {
                return ApplicationResult<Domain.Entity.User>.BadRequest(validationErrors);
            }

            var user = new Domain.Entity.User { FirstName = request.FirstName, LastName = request.LastName };

            _databaseContext.User.Add(user);

            await _databaseContext.SaveChangesAsync(cancellationToken);

            return ApplicationResult<Domain.Entity.User>.Ok(user);
        }
    }
}
