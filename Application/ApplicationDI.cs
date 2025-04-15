using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using Application.Common.Behaviors;
using Application.Features.Orders;

namespace Application
{
    public static class ApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IAmountCalculator, BaseAmountCalculator>();
            services.AddScoped<IShipFeeCalculator, MockShipFeeCalculator>();
            services.AddScoped<UpdateCustomerProcess>();
            return services;
        }
    }
}
