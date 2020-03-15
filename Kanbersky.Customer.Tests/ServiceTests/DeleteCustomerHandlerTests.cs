using AutoFixture;
using FluentAssertions;
using Kanbersky.Customer.Business.Commands;
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
    public class DeleteCustomerHandlerTests
    {
        #region fields

        private readonly DeleteCustomerCommandHandler _sut;
        private readonly Mock<IGenericRepository<Entity.Concrete.Customer>> _repository;
        private readonly Entity.Concrete.Customer _customer;
        private readonly Mock<IResult> _result;
        private readonly Fixture _fixture = new Fixture();
        private readonly DeleteCustomerCommand _deleteCommand;
        private readonly CancellationToken cancellationToken = new CancellationToken();

        #endregion

        #region ctor

        public DeleteCustomerHandlerTests()
        {
            _customer = _fixture.Create<Entity.Concrete.Customer>();
            _result = new Mock<IResult>();
            _repository = new Mock<IGenericRepository<Entity.Concrete.Customer>>();
            _deleteCommand = _fixture.Create<DeleteCustomerCommand>();
            _sut = new DeleteCustomerCommandHandler(_repository.Object);
        }

        #endregion

        #region methods

        [Fact]
        public void Delete_Customer_Should_Success()
        {
            //Arrange
            _repository.Setup(c => c.Get(It.IsAny<Expression<Func<Entity.Concrete.Customer, bool>>>())).Returns(Task.FromResult(_customer));
            _repository.Setup(c => c.Delete(_customer));
            _repository.Setup(c => c.SaveChangesAsync()).Returns(Task.FromResult(1));
            _result.Setup(t => t.Success).Returns(true);

            //Act
            var actual = _sut.Handle(_deleteCommand,cancellationToken).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Be(typeof(SuccessResult));
        }

        [Fact]
        public void Delete_Customer_Should_Error()
        {
            //Arrange
            _repository.Setup(c => c.Get(It.IsAny<Expression<Func<Entity.Concrete.Customer, bool>>>()));
            _repository.Setup(c => c.Delete(_customer));
            _repository.Setup(c => c.SaveChangesAsync()).Returns(Task.FromResult(0));
            _result.Setup(t => t.Success).Returns(false);

            //Act
            var actual = _sut.Handle(_deleteCommand, cancellationToken).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Be(typeof(ErrorResult));
        }

        #endregion
    }
}
