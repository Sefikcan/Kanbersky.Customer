using Kanbersky.Customer.Business.Commands;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Kanbersky.Customer.Core.Results;
using Microsoft.AspNetCore.Http;

namespace Kanbersky.Customer.Business.Handlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, IResult>
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

        public async Task<IResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.Get(x => x.Id == request.Id);
            if (response != null)
            {
                _repository.Delete(response);
                if (await _repository.SaveChangesAsync() > 0)
                {
                    return new SuccessResult(StatusCodes.Status204NoContent);
                }
            }

            return new ErrorResult("Entity Remove Failed",StatusCodes.Status400BadRequest);
        }

        #endregion
    }
}
