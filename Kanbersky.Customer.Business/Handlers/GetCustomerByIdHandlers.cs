using AutoMapper;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Business.Queries;
using Kanbersky.Customer.Core.Results;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.Customer.Business.Handlers
{
    public class GetCustomerByIdHandlers : IRequestHandler<GetCustomerByIdQuery, IDataResult<GetCustomerByIdQueryResponse>>
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

        public async Task<IDataResult<GetCustomerByIdQueryResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _repository.Get(x => x.Id == request.Id);
            if (response != null)
            {
                return new SuccessDataResult<GetCustomerByIdQueryResponse>(_mapper.Map<GetCustomerByIdQueryResponse>(response), StatusCodes.Status200OK);
            }

            return new ErrorDataResult<GetCustomerByIdQueryResponse>("Customer Not Found",_mapper.Map<GetCustomerByIdQueryResponse>(response), StatusCodes.Status400BadRequest);
        }

        #endregion
    }
}
