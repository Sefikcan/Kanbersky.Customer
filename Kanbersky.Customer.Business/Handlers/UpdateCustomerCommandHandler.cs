using AutoMapper;
using Kanbersky.Customer.Business.Commands;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.Customer.Business.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
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

        public async Task<UpdateCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.Get(x => x.Id == request.UpdateCustomerRequest.Id);
            if (response != null)
            {
                var customer = _mapper.Map<Entity.Concrete.Customer>(request.UpdateCustomerRequest);
                var updateResult = await _repository.Update(customer);
                var entityResponse = await _repository.SaveChangesAsync();
                if (entityResponse > 0)
                {
                    return _mapper.Map<UpdateCustomerResponse>(response);
                }
            }

            throw new System.Exception("Update Fail!");
        }

        #endregion
    }
}
