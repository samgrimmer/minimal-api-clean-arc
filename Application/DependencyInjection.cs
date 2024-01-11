using Application.Features.User.Command;
using Application.Features.User.Query;
using Application.Service;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<GetUserQuery>());
            
            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();

            services.AddTransient<IValidatorService<CreateUserCommand>, ValidatorService<CreateUserCommand>>();

            return services;
        }
    }
}
