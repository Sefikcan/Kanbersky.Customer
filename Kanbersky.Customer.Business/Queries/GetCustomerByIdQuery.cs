using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Core.Results;
using MediatR;

namespace Kanbersky.Customer.Business.Queries
{
    public class GetCustomerByIdQuery : IRequest<IDataResult<GetCustomerByIdQueryResponse>>
    {
        public int Id { get; set; }

        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
