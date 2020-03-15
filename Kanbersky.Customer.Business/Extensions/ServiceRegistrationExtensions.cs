using FluentValidation;
using Kanbersky.Customer.Business.DTO.Request;
using Kanbersky.Customer.Business.Handlers;
using Kanbersky.Customer.Business.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Kanbersky.Customer.Business.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void RegisterHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetAllCustomersQueryHandler), typeof(GetCustomerByIdHandlers), typeof(DeleteCustomerCommandHandler), typeof(CreateCustomerCommandHandler), typeof(UpdateCustomerCommandHandler));
        }

        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();
            services.AddSingleton<IValidator<UpdateCustomerRequest>, UpdateCustomerRequestValidator>();
        }
    }
}
