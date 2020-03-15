using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Kanbersky.Customer.Business.Commands;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Business.Handlers;
using Kanbersky.Customer.Core.Results;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Kanbersky.Customer.Tests.ServiceTests
{
    public class CreateCustomerHandlerTests
    {
        #region fields

        private readonly CreateCustomerCommandHandler _sut;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IGenericRepository<Entity.Concrete.Customer>> _repository;
        private readonly Entity.Concrete.Customer _customer;
        private readonly Fixture _fixture = new Fixture();
        private readonly CreateCustomerCommand _createCommand;
        private readonly CancellationToken cancellationToken = new CancellationToken();

        #endregion

        #region ctor

        public CreateCustomerHandlerTests()
        {
            _mapper = new Mock<IMapper>();
            _customer = _fixture.Create<Entity.Concrete.Customer>();
            _repository = new Mock<IGenericRepository<Entity.Concrete.Customer>>();
            _createCommand = _fixture.Create<CreateCustomerCommand>();
            _sut = new CreateCustomerCommandHandler(_repository.Object,_mapper.Object);
        }

        #endregion

        #region methods

        [Fact]
        public void Insert_Customer_Should_Success()
        {
            //Arrange
            _mapper.Setup(x => x.Map<Entity.Concrete.Customer>(_createCommand)).Returns(_customer);
            _repository.Setup(c => c.Add(_customer)).Returns(Task.FromResult(_customer));
            _repository.Setup(c => c.SaveChangesAsync()).Returns(Task.FromResult(1));

            //Act
            var actual = _sut.Handle(_createCommand, cancellationToken).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Be(typeof(SuccessDataResult<CreateCustomerResponse>));
        }

        [Fact]
        public void Insert_Customer_Should_Error()
        {
            //Arrange
            _mapper.Setup(x => x.Map<Entity.Concrete.Customer>(_createCommand)).Returns(_customer);
            _repository.Setup(c => c.Add(_customer)).Returns(Task.FromResult(_customer));
            _repository.Setup(c => c.SaveChangesAsync()).Returns(Task.FromResult(0));

            //Act
            var actual = _sut.Handle(_createCommand,cancellationToken).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Be(typeof(ErrorDataResult<CreateCustomerResponse>));
        }

        #endregion
    }
}
