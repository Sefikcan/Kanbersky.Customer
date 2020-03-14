using AutoMapper;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Business.Queries;
using Kanbersky.Customer.Core.Results;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.Customer.Business.Handlers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IDataResult<List<GetAllCustomerQueryResponse>>>
    {
        #region fields

        private readonly IGenericRepository<Entity.Concrete.Customer> _repository;
        private readonly IMapper _mapper;

        #endregion

        #region ctor

        public GetAllCustomersQueryHandler(IGenericRepository<Entity.Concrete.Customer> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #endregion

        #region methods

        public async Task<IDataResult<List<GetAllCustomerQueryResponse>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetList();
            if (response.Count>0)
            {
                return new SuccessDataResult<List<GetAllCustomerQueryResponse>>(_mapper.Map<List<GetAllCustomerQueryResponse>>(response),StatusCodes.Status200OK);
            }

            return new ErrorDataResult<List<GetAllCustomerQueryResponse>>("Customer Not Found",_mapper.Map<List<GetAllCustomerQueryResponse>>(response), StatusCodes.Status404NotFound);
        }

        #endregion
    }
}
