using Kanbersky.Customer.Business.DTO.Response;
using MediatR;

namespace Kanbersky.Customer.Business.Queries
{
    public class GetCustomerByIdQuery : IRequest<GetCustomerByIdQueryResponse>
    {
        public int Id { get; set; }

        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
