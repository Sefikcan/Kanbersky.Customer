using MediatR;

namespace Kanbersky.Customer.Business.Commands
{
    public class DeleteCustomerCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteCustomerCommand(int id)
        {
            Id = id;
        }
    }
}
