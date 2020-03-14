using Kanbersky.Customer.Core.Results;
using MediatR;

namespace Kanbersky.Customer.Business.Commands
{
    public class DeleteCustomerCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public DeleteCustomerCommand(int id)
        {
            Id = id;
        }
    }
}
