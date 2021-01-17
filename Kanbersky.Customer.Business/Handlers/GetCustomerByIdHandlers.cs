using AutoMapper;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Business.Queries;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.Customer.Business.Handlers
{
    public class GetCustomerByIdHandlers : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdQueryResponse>
    {
        #region fields

        private readonly IGenericRepository<Entity.Concrete.Customer> _repository;
        private readonly IMapper _mapper;

        #endregion

        #region ctor

        public GetCustomerByIdHandlers(IGenericRepository<Entity.Concrete.Customer> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #endregion

        #region methods

        public async Task<GetCustomerByIdQueryResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.Get(x => x.Id == request.Id);
            if (response != null)
            {
                return _mapper.Map<GetCustomerByIdQueryResponse>(response);
            }

            throw new System.Exception("Customer Not Found!");
        }

        #endregion
    }
}
