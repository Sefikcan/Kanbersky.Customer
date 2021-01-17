using Kanbersky.Customer.Business.DTO.Request;
using Kanbersky.Customer.Business.DTO.Response;
using MediatR;

namespace Kanbersky.Customer.Business.Commands
{
    public class UpdateCustomerCommand : IRequest<UpdateCustomerResponse>
    {
        public UpdateCustomerRequest UpdateCustomerRequest { get; set; }

        public UpdateCustomerCommand(UpdateCustomerRequest updateCustomerRequest)
        {
            UpdateCustomerRequest = updateCustomerRequest;
        }
    }
}
