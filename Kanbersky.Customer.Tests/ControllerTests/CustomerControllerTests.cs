using AutoFixture;
using FluentAssertions;
using Kanbersky.Customer.Api.Controllers;
using Kanbersky.Customer.Business.Commands;
using Kanbersky.Customer.Business.DTO.Request;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Business.Queries;
using Kanbersky.Customer.Core.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Kanbersky.Customer.Tests.ControllerTests
{
    public class CustomerControllerTests
    {
        #region fields

        private readonly CustomersController _sut;
        private readonly Mock<IMediator> _mediator;

        private readonly CreateCustomerRequest _createCustomerRequest;
        private readonly UpdateCustomerRequest _updateCustomerRequest;

        readonly Mock<IDataResult<List<GetAllCustomerQueryResponse>>> _customerDataListResponse = new Mock<IDataResult<List<GetAllCustomerQueryResponse>>>();
        private readonly Mock<List<GetAllCustomerQueryResponse>> _customerListResponse;

        readonly Mock<IDataResult<GetCustomerByIdQueryResponse>> _customerDataResponse = new Mock<IDataResult<GetCustomerByIdQueryResponse>>();
        private readonly GetCustomerByIdQueryResponse _customerResponse;

        private readonly Mock<IDataResult<CreateCustomerResponse>> _createCustomerDataResponse;
        private readonly Mock<CreateCustomerResponse> _createCustomerResponse;

        private readonly Mock<IDataResult<UpdateCustomerResponse>> _updateCustomerDataResponse;
        private readonly Mock<UpdateCustomerResponse> _updateCustomerResponse;

        private readonly Mock<IResult> _result;

        private readonly Fixture _fixture = new Fixture();
        private readonly int _customerId;

        #endregion

        #region ctor

        public CustomerControllerTests()
        {
            _customerId = _fixture.Create<int>();
            _result = new Mock<IResult>();
            _updateCustomerRequest = _fixture.Create<UpdateCustomerRequest>();
            _createCustomerRequest = _fixture.Create<CreateCustomerRequest>();
            _createCustomerDataResponse = new Mock<IDataResult<CreateCustomerResponse>>();
            _createCustomerResponse = new Mock<CreateCustomerResponse>();
            _updateCustomerDataResponse = new Mock<IDataResult<UpdateCustomerResponse>>();
            _updateCustomerResponse = new Mock<UpdateCustomerResponse>();
            _customerResponse = _fixture.Create<GetCustomerByIdQueryResponse>();
            _customerListResponse = new Mock<List<GetAllCustomerQueryResponse>>();
            _mediator = new Mock<IMediator>();
            _sut = new CustomersController(_mediator.Object);
        }

        #endregion

        #region methods

        [Fact]
        public void GetAll_Should_Return_As_200OKResult()
        {
            //Arrange
            _mediator.Setup(s => s.Send(It.IsAny<GetAllCustomersQuery>(), default)).Returns(Task.FromResult(_customerDataListResponse.Object));
            _customerDataListResponse.Setup(t => t.Success).Returns(true);
            _customerDataListResponse.Setup(t => t.StatusCode).Returns(StatusCodes.Status200OK);
            _customerDataListResponse.Setup(s => s.Data).Returns(_customerListResponse.Object);
            //Act
            var actual = _sut.GetAllCustomers().GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Equals(StatusCodes.Status200OK);
        }

        [Fact]
        public void GetAll_Should_Return_As_404NotFoundResult()
        {
            //Arrange
            _mediator.Setup(s => s.Send(It.IsAny<GetAllCustomersQuery>(), default)).Returns(Task.FromResult(_customerDataListResponse.Object));
            _customerDataListResponse.Setup(t => t.Success).Returns(false);
            _customerDataListResponse.Setup(t => t.StatusCode).Returns(StatusCodes.Status404NotFound);
            _customerDataListResponse.Setup(s => s.Data).Returns(_customerListResponse.Object);
            //Act
            var actual = _sut.GetAllCustomers().GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Equals(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void GetById_Should_Return_As_200OKResult()
        {
            //Arrange
            _mediator.Setup(s => s.Send(It.IsAny<GetCustomerByIdQuery>(), default)).Returns(Task.FromResult(_customerDataResponse.Object));
            _customerDataResponse.Setup(t => t.Success).Returns(true);
            _customerDataResponse.Setup(t => t.StatusCode).Returns(StatusCodes.Status200OK);
            _customerDataResponse.Setup(s => s.Data).Returns(_customerResponse);

            //Act
            var actual = _sut.GetCustomerById(_customerId).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Equals(StatusCodes.Status200OK);
        }

        [Fact]
        public void GetById_Should_Return_As_404NotFoundResult()
        {
            //Arrange
            _mediator.Setup(s => s.Send(It.IsAny<GetCustomerByIdQuery>(), default)).Returns(Task.FromResult(_customerDataResponse.Object));
            _customerDataResponse.Setup(t => t.Success).Returns(false);
            _customerDataResponse.Setup(t => t.StatusCode).Returns(StatusCodes.Status404NotFound);
            _customerDataResponse.Setup(s => s.Data).Returns(_customerResponse);

            //Act
            var actual = _sut.GetCustomerById(_customerId).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Equals(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void Create_Should_Return_As_201CreatedResult()
        {
            //Arrange
            _mediator.Setup(s => s.Send(It.IsAny<CreateCustomerCommand>(), default)).Returns(Task.FromResult(_createCustomerDataResponse.Object));
            _createCustomerDataResponse.Setup(t => t.Success).Returns(true);
            _createCustomerDataResponse.Setup(t => t.StatusCode).Returns(StatusCodes.Status201Created);
            _createCustomerDataResponse.Setup(s => s.Data).Returns(_createCustomerResponse.Object);
            //Act
            var actual = _sut.CreateCustomer(_createCustomerRequest).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Equals(StatusCodes.Status201Created);
        }

        [Fact]
        public void Create_Should_Return_As_400BadRequestResult()
        {
            //Arrange
            _mediator.Setup(s => s.Send(It.IsAny<CreateCustomerCommand>(), default)).Returns(Task.FromResult(_createCustomerDataResponse.Object));
            _createCustomerDataResponse.Setup(t => t.Success).Returns(false);
            _createCustomerDataResponse.Setup(t => t.StatusCode).Returns(StatusCodes.Status400BadRequest);
            _createCustomerDataResponse.Setup(s => s.Data).Returns(_createCustomerResponse.Object);
            //Act
            var actual = _sut.CreateCustomer(_createCustomerRequest).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Equals(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public void Update_Should_Return_As_200OKResult()
        {
            //Arrange
            _mediator.Setup(s => s.Send(It.IsAny<UpdateCustomerCommand>(), default)).Returns(Task.FromResult(_updateCustomerDataResponse.Object));
            _updateCustomerDataResponse.Setup(t => t.Success).Returns(true);
            _updateCustomerDataResponse.Setup(t => t.StatusCode).Returns(StatusCodes.Status200OK);
            _updateCustomerDataResponse.Setup(s => s.Data).Returns(_updateCustomerResponse.Object);
            //Act
            var actual = _sut.UpdateCustomer(_updateCustomerRequest).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Equals(StatusCodes.Status200OK);
        }

        [Fact]
        public void Update_Should_Return_As_400BadRequestResult()
        {
            //Arrange
            _mediator.Setup(s => s.Send(It.IsAny<UpdateCustomerCommand>(), default)).Returns(Task.FromResult(_updateCustomerDataResponse.Object));
            _updateCustomerDataResponse.Setup(t => t.Success).Returns(false);
            _updateCustomerDataResponse.Setup(t => t.StatusCode).Returns(StatusCodes.Status400BadRequest);
            _updateCustomerDataResponse.Setup(s => s.Data).Returns(_updateCustomerResponse.Object);
            //Act
            var actual = _sut.UpdateCustomer(_updateCustomerRequest).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Equals(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public void Delete_Should_Return_As_204NoContentResult()
        {
            //Arrange
            _mediator.Setup(s => s.Send(It.IsAny<DeleteCustomerCommand>(), default)).Returns(Task.FromResult(_result.Object));
            _result.Setup(t => t.Success).Returns(true);
            _result.Setup(t => t.StatusCode).Returns(StatusCodes.Status204NoContent);

            //Act
            var actual = _sut.DeleteCustomer(_customerId).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Equals(StatusCodes.Status204NoContent);
        }

        [Fact]
        public void Delete_Should_Return_As_400BadRequestResult()
        {
            //Arrange
            _mediator.Setup(s => s.Send(It.IsAny<DeleteCustomerCommand>(), default)).Returns(Task.FromResult(_result.Object));
            _result.Setup(t => t.Success).Returns(false);
            _result.Setup(t => t.StatusCode).Returns(StatusCodes.Status400BadRequest);

            //Act
            var actual = _sut.DeleteCustomer(_customerId).GetAwaiter().GetResult();

            //Assert
            actual.GetType().Should().Equals(StatusCodes.Status400BadRequest);
        }

        #endregion
    }
}
