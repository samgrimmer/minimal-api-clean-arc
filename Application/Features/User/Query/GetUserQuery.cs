using Application.Models;
using Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.User.Query
{
    public record GetUserQuery(int id) : IRequest<ApplicationResult<Domain.Entity.User>>;

    public class GetUserHandler : IRequestHandler<GetUserQuery, ApplicationResult<Domain.Entity.User>>
    {
        private readonly DatabaseContext _databaseContext;

        public GetUserHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<ApplicationResult<Domain.Entity.User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _databaseContext.User
                .AsNoTracking()
                    .Where(u => u.Id == request.id)
                        .SingleOrDefaultAsync(cancellationToken);

            return user == null ? ApplicationResult<Domain.Entity.User>.NotFound() : ApplicationResult<Domain.Entity.User>.Ok(user);
        }
    }
}
