using Kanbersky.Customer.Business.Commands;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.Customer.Business.Handlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand,Unit>
    {
        #region fields

        private readonly IGenericRepository<Entity.Concrete.Customer> _repository;

        #endregion

        #region ctor

        public DeleteCustomerCommandHandler(IGenericRepository<Entity.Concrete.Customer> repository)
        {
            _repository = repository;
        }

        #endregion

        #region methods

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.Get(x => x.Id == request.Id);
            if (response != null)
            {
                _repository.Delete(response);
                await _repository.SaveChangesAsync();
            }

            throw new System.Exception("Customer Not Found!");
        }

        #endregion
    }
}
