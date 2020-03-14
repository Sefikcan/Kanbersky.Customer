using Kanbersky.Customer.Business.DTO.Request;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Core.Results;
using MediatR;

namespace Kanbersky.Customer.Business.Commands
{
    public class UpdateCustomerCommand : IRequest<IDataResult<UpdateCustomerResponse>>
    {
        public UpdateCustomerRequest UpdateCustomerRequest { get; set; }

        public UpdateCustomerCommand(UpdateCustomerRequest updateCustomerRequest)
        {
            UpdateCustomerRequest = updateCustomerRequest;
        }
    }
}
