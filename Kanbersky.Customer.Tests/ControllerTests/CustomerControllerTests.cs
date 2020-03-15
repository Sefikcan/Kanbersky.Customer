using FluentAssertions;
using Kanbersky.Customer.Api.Controllers;
using Kanbersky.Customer.Business.DTO.Response;
using Kanbersky.Customer.Business.Queries;
using Kanbersky.Customer.Core.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kanbersky.Customer.Tests.ControllerTests
{
    public class CustomerControllerTests
    {
        #region fields

        private readonly CustomersController _sut;
        private readonly Mock<IMediator> _mediator;
        Mock<IDataResult<List<GetAllCustomerQueryResponse>>> _customerListResponse = new Mock<IDataResult<List<GetAllCustomerQueryResponse>>>();


        #endregion

        #region ctor

        public CustomerControllerTests(Mock<IMediator> mediator)
        {
            _mediator = mediator;
            _sut = new CustomersController(_mediator.Object);
        }

        #endregion

        #region methods

        //[Fact]
        //public void GetAll_Should_Return_As_Expected()
        //{
        //    //Arrange
        //    _mediator.Setup(s => s.Send(new GetAllCustomersQuery())).Returns(Task.FromResult(_customerListResponse.Object));

        //    //Act
        //    var actual = _sut.GetAllCustomers().GetAwaiter().GetResult();

        //    //Assert
        //    actual.GetType().Should().Be(typeof(OkResult));
        //}

        //[Fact]
        //public void GetById_Should_Return_As_Expected()
        //{
        //    //Arrange
        //    _mediator.Setup(s => s.Send(It.IsAny<GetCustomerByIdQuery>(new { Id = 1 }))).Returns(Task.FromResult(_customerListResponse.Object));

        //    //Act
        //    var actual = _sut.GetAllCustomers().GetAwaiter().GetResult();

        //    //Assert
        //    actual.GetType().Should().Be(typeof(OkResult));
        //}

        //[Fact]
        //public void Create_Should_Return_As_Expected()
        //{

        //}

        //[Fact]
        //public void Update_Should_Return_As_Expected()
        //{

        //}

        //[Fact]
        //public void Delete_Should_Return_As_Expected()
        //{

        //}

        #endregion
    }
}
