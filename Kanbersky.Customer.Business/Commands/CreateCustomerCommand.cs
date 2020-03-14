using Kanbersky.Customer.Business.DTO.Request;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Core.Results;
using MediatR;

namespace Kanbersky.Customer.Business.Commands
{
    public class CreateCustomerCommand : IRequest<IDataResult<CreateCustomerResponse>>
    {
        public CreateCustomerRequest CreateCustomerRequest { get; set; }

        public CreateCustomerCommand(CreateCustomerRequest createCustomerRequest)
        {
            CreateCustomerRequest = createCustomerRequest;
        }
    }
}
