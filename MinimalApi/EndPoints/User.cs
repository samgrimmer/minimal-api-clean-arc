using Application.Enumeration;
using Application.Features.User.Command;
using Application.Features.User.Query;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApi.Interfaces;

namespace MinimalApi.EndPoints
{
    public class User : IEndPointsDefinition
    {
        public void RegisterEndPoints(WebApplication app)
        {
            var group = app.MapGroup("/api/user");

            group.MapGet("{id}", GetUser);
            group.MapPost("", CreateUser);
        }

        private async Task<Results<Ok<Domain.Entity.User>, NotFound>> GetUser(IMediator mediator, int id)
        {
            var query = new GetUserQuery(id);

            var result = await mediator.Send(query);

            return result.Status == ApplicationResultStatus.Ok ? TypedResults.Ok(result.Data) : TypedResults.NotFound();
        }

        private async Task<Results<Ok<Domain.Entity.User>, BadRequest<List<Error>>>> CreateUser(IMediator mediator, Domain.Entity.User user)
        {
            var command = new CreateUserCommand { FirstName = user.FirstName, LastName = user.LastName };

            var result = await mediator.Send(command);

            return result.Status == ApplicationResultStatus.Ok ? TypedResults.Ok(result.Data) : TypedResults.BadRequest(result.ValidationErrors);
        }
    }
}
