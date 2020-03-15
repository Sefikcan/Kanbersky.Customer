using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Business.Handlers;
using Kanbersky.Customer.Business.Queries;
using Kanbersky.Customer.Core.Results;
using Kanbersky.Customer.DAL.Concrete.EntityFramework.GenericRepository;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Kanbersky.Customer.Tests.ServiceTests
{
    public class GetAllCustomerHandlerTests
    {
        #region fields

        private readonly GetAllCustomersQueryHandler _sut;
        private readonly Mock<IGenericRepository<Entity.Concrete.Customer>> _repository;
        private readonly Mock<IMapper> _mapper;
        private readonly List<Entity.Concrete.Customer> _customerList;
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<List<GetAllCustomerQueryResponse>> _customerListResponse;
        private readonly Mock<IDataResult<List<GetAllCustomerQueryResponse>>> _customerListDataResponse;
        private readonly GetAllCustomersQuery getAllCustomersQuery = new GetAllCustomersQuery();
        private readonly CancellationToken cancellationToken = new CancellationToken();

        #endregion

        #region ctor

        public GetAllCustomerHandlerTests()
        {
            _customerList = _fixture.Create<List<Entity.Concrete.Customer>>();
            _customerListResponse = new Mock<List<GetAllCustomerQueryResponse>>();
            _customerListDataResponse = new Mock<IDataResult<List<GetAllCustomerQueryResponse>>>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<IGenericRepository<Entity.Concrete.Customer>>();
            _sut = new GetAllCustomersQueryHandler(_repository.Object, _mapper.Object);
        }

        #endregion

        #region methods

        [Fact]
        public void GetAll_Should_Return_As_Expected()
        {
            //Arrange
            _repository.Setup(c => c.GetList(null)).Returns(Task.FromResult(_customerList));
            _mapper.Setup(x => x.Map<List<Entity.Concrete.Customer>>(null)).Returns(_customerList);
            _customerListDataResponse.Setup(t => t.Success).Returns(true);
            _customerListDataResponse.Setup(s => s.Data).Returns(_customerListResponse.Object);

            //Act
            var actual = _sut.Handle(getAllCustomersQuery, cancellationToken).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Be(typeof(SuccessDataResult<List<GetAllCustomerQueryResponse>>));
        }

        [Fact]
        public void GetAll_Customer_Should_Error()
        {
            //Arrange
            List<Entity.Concrete.Customer> _customerCountZeero = new List<Entity.Concrete.Customer>();
            _repository.Setup(c => c.GetList(null)).Returns(Task.FromResult(_customerCountZeero));
            _customerListDataResponse.Setup(t => t.Success).Returns(false);
            _customerListDataResponse.Setup(t => t.Data).Equals(new List<Entity.Concrete.Customer>());

            //Act
            var actual = _sut.Handle(getAllCustomersQuery, cancellationToken).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Be(typeof(ErrorDataResult<List<GetAllCustomerQueryResponse>>));
        }

        #endregion
    }
}
