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
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, IDataResult<UpdateCustomerResponse>>
    {
        #region fields

        private readonly IGenericRepository<Entity.Concrete.Customer> _repository;
        private readonly IMapper _mapper;

        #endregion

        #region ctor

        public UpdateCustomerCommandHandler(IGenericRepository<Entity.Concrete.Customer> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #endregion

        #region methods

        public async Task<IDataResult<UpdateCustomerResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.Get(x => x.Id == request.UpdateCustomerRequest.Id);
            if (response != null)
            {
                var customer = _mapper.Map<Entity.Concrete.Customer>(request.UpdateCustomerRequest);
                var updateResult = await _repository.Update(customer);
                var entityResponse = await _repository.SaveChangesAsync();
                if (entityResponse > 0)
                {
                    return new SuccessDataResult<UpdateCustomerResponse>(_mapper.Map<UpdateCustomerResponse>(response), StatusCodes.Status200OK);
                }
            }

            return new ErrorDataResult<UpdateCustomerResponse>("Entity Update Fail",_mapper.Map<UpdateCustomerResponse>(response), StatusCodes.Status400BadRequest);
        }

        #endregion
    }
}
