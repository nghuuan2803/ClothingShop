using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            var assembly = typeof(ICommand<,>).Assembly;
            var commandTypes = assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommand<,>))
                    .Select(i => new { Interface = i, Implementation = t }));

            foreach (var cmd in commandTypes)
            {
                services.AddScoped(cmd.Interface, cmd.Implementation);
            }

            return services;
        }
    }
}
