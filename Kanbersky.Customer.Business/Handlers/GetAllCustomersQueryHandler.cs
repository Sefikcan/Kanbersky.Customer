using AutoMapper;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Business.Queries;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.Customer.Business.Handlers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<GetAllCustomerQueryResponse>>
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

        public async Task<List<GetAllCustomerQueryResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.GetList();
            if (response.Count>0)
            {
                return _mapper.Map<List<GetAllCustomerQueryResponse>>(response);
            }

            throw new System.Exception("Customer Not Found!");
        }

        #endregion
    }
}
