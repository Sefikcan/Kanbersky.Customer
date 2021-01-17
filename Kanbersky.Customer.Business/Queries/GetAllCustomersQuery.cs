using Kanbersky.Customer.Business.DTO.Response;
using MediatR;
using System.Collections.Generic;

namespace Kanbersky.Customer.Business.Queries
{
    public class GetAllCustomersQuery : IRequest<List<GetAllCustomerQueryResponse>>
    {
    }
}
