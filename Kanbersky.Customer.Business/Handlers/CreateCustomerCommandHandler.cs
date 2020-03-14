using AutoMapper;
using Kanbersky.Customer.Business.Commands;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Core.Results;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.Customer.Business.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, IDataResult<CreateCustomerResponse>>
    {
        #region fields

        private readonly IGenericRepository<Entity.Concrete.Customer> _repository;
        private readonly IMapper _mapper;

        #endregion

        #region ctor

        public CreateCustomerCommandHandler(IGenericRepository<Entity.Concrete.Customer> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #endregion

        #region methods

        public async Task<IDataResult<CreateCustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Entity.Concrete.Customer>(request.CreateCustomerRequest);
            var response = await _repository.Add(customer);
            var entityResponse = await _repository.SaveChangesAsync();
            if (entityResponse > 0)
            {
                return new SuccessDataResult<CreateCustomerResponse>(_mapper.Map<CreateCustomerResponse>(response), StatusCodes.Status201Created);
            }

            return new ErrorDataResult<CreateCustomerResponse>("Entity Insert Fail",_mapper.Map<CreateCustomerResponse>(response), StatusCodes.Status400BadRequest);
        }

        #endregion
    }
}
