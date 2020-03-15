using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Kanbersky.Customer.Business.Commands;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Business.Handlers;
using Kanbersky.Customer.Core.Results;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Kanbersky.Customer.Tests.ServiceTests
{
    public class UpdateCustomerHandlerTests
    {
        #region fields

        private readonly UpdateCustomerCommandHandler _sut;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IGenericRepository<Entity.Concrete.Customer>> _repository;
        private readonly Entity.Concrete.Customer _customer;
        private readonly Fixture _fixture = new Fixture();
        private readonly UpdateCustomerCommand _updateCommand;
        private readonly CancellationToken cancellationToken = new CancellationToken();

        #endregion

        #region ctor

        public UpdateCustomerHandlerTests()
        {
            _mapper = new Mock<IMapper>();
            _customer = _fixture.Create<Entity.Concrete.Customer>();
            _repository = new Mock<IGenericRepository<Entity.Concrete.Customer>>();
            _updateCommand = _fixture.Create<UpdateCustomerCommand>();
            _sut = new UpdateCustomerCommandHandler(_repository.Object, _mapper.Object);
        }

        #endregion

        #region methods

        [Fact]
        public void Update_Customer_Should_Success()
        {
            //Arrange
            _repository.Setup(c => c.Get(It.IsAny<Expression<Func<Entity.Concrete.Customer, bool>>>())).Returns(Task.FromResult(_customer));
            _mapper.Setup(x => x.Map<Entity.Concrete.Customer>(_updateCommand)).Returns(_customer);
            _repository.Setup(c => c.Update(_customer)).Returns(Task.FromResult(_customer));
            _repository.Setup(c => c.SaveChangesAsync()).Returns(Task.FromResult(1));

            //Act
            var actual = _sut.Handle(_updateCommand, cancellationToken).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Be(typeof(SuccessDataResult<UpdateCustomerResponse>));
        }

        [Fact]
        public void Insert_Customer_Should_Error()
        {
            //Arrange
            _repository.Setup(c => c.Get(It.IsAny<Expression<Func<Entity.Concrete.Customer, bool>>>()));
            _mapper.Setup(x => x.Map<Entity.Concrete.Customer>(_updateCommand)).Returns(_customer);
            _repository.Setup(c => c.Update(_customer)).Returns(Task.FromResult(_customer));
            _repository.Setup(c => c.SaveChangesAsync()).Returns(Task.FromResult(0));

            //Act
            var actual = _sut.Handle(_updateCommand, cancellationToken).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Be(typeof(ErrorDataResult<UpdateCustomerResponse>));
        }

        #endregion
    }
}
