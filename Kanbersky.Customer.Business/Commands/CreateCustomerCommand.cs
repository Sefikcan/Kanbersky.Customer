using Kanbersky.Customer.Business.DTO.Request;
using Kanbersky.Customer.Business.DTO.Response;
using MediatR;

namespace Kanbersky.Customer.Business.Commands
{
    public class CreateCustomerCommand : IRequest<CreateCustomerResponse>
    {
        public CreateCustomerRequest CreateCustomerRequest { get; set; }

        public CreateCustomerCommand(CreateCustomerRequest createCustomerRequest)
        {
            CreateCustomerRequest = createCustomerRequest;
        }
    }
}
