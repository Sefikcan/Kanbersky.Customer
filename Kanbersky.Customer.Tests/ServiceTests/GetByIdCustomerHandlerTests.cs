using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Business.Handlers;
using Kanbersky.Customer.Business.Queries;
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
    public class GetByIdCustomerHandlerTests
    {
        #region fields

        private readonly GetCustomerByIdHandlers _sut;
        private readonly Mock<IGenericRepository<Entity.Concrete.Customer>> _repository;
        private readonly Mock<IMapper> _mapper;
        private readonly Entity.Concrete.Customer _customer;
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IDataResult<GetCustomerByIdQueryResponse>> _customerDataResponse;
        private readonly Mock<GetCustomerByIdQueryResponse> _customerResponse;
        private readonly GetCustomerByIdQuery _customerIdQuery;
        private readonly CancellationToken cancellationToken = new CancellationToken();

        #endregion

        #region ctor

        public GetByIdCustomerHandlerTests()
        {
            _customer = _fixture.Create<Entity.Concrete.Customer>();
            _customerIdQuery = _fixture.Create<GetCustomerByIdQuery>();
            _customerResponse = new Mock<GetCustomerByIdQueryResponse>();
            _customerDataResponse = new Mock<IDataResult<GetCustomerByIdQueryResponse>>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<IGenericRepository<Entity.Concrete.Customer>>();
            _sut = new GetCustomerByIdHandlers(_repository.Object, _mapper.Object);
        }

        #endregion

        #region methods

        [Fact]
        public void GetById_Should_Return_As_Expected()
        {
            //Arrange
            _repository.Setup(c => c.Get(It.IsAny<Expression<Func<Entity.Concrete.Customer, bool>>>())).Returns(Task.FromResult(_customer));
            _mapper.Setup(x => x.Map<Entity.Concrete.Customer>(null)).Returns(_customer);
            _customerDataResponse.Setup(t => t.Success).Returns(true);
            _customerDataResponse.Setup(s => s.Data).Returns(_customerResponse.Object);

            //Act
            var actual = _sut.Handle(_customerIdQuery,cancellationToken).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Be(typeof(SuccessDataResult<GetCustomerByIdQueryResponse>));
        }

        [Fact]
        public void GetById_Customer_Should_Error()
        {
            //Arrange
            _repository.Setup(c => c.Get(It.IsAny<Expression<Func<Entity.Concrete.Customer, bool>>>()));
            _customerDataResponse.Setup(t => t.Success).Returns(false);
            _customerDataResponse.Setup(t => t.Data).Equals(new Entity.Concrete.Customer());

            //Act
            var actual = _sut.Handle(_customerIdQuery, cancellationToken).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Be(typeof(ErrorDataResult<GetCustomerByIdQueryResponse>));
        }

        #endregion
    }
}
