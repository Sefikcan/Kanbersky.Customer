using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Core.Results;
using MediatR;
using System.Collections.Generic;

namespace Kanbersky.Customer.Business.Queries
{
    public class GetAllCustomersQuery : IRequest<IDataResult<List<GetAllCustomerQueryResponse>>>
    {
    }
}
