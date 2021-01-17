using FluentValidation;
using Kanbersky.Customer.Business.DTO.Request;
using Kanbersky.Customer.Business.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kanbersky.Customer.Business.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void RegisterHandlers(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();
            services.AddSingleton<IValidator<UpdateCustomerRequest>, UpdateCustomerRequestValidator>();
        }
    }
}
