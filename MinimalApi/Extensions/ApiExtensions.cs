using FluentValidation;
using MinimalApi.Interfaces;

namespace MinimalApi.Extensions
{
    public static class ApiExtensions
    {
        public static void RegisterEndPointDefinitions(this WebApplication app) 
        {
            var endpointDefinitions = typeof(Program).Assembly
                .GetTypes()
                .Where(e => e.IsAssignableTo(typeof(IEndPointsDefinition)) && !e.IsAbstract && !e.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndPointsDefinition>();

            foreach(var endpointDefinition in endpointDefinitions)
            {
                endpointDefinition.RegisterEndPoints(app);
            }
        }
    }
}
