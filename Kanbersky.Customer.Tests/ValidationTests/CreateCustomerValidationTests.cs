using FluentAssertions;
using Kanbersky.Customer.Business.DTO.Request;
using Kanbersky.Customer.Business.Validators;
using Xunit;

namespace Kanbersky.Customer.Tests.ValidationTests
{
    public class CreateCustomerValidationTests
    {
        #region fields

        private CreateCustomerRequestValidator _validators;

        #endregion

        #region ctor

        public CreateCustomerValidationTests()
        {
            _validators = new CreateCustomerRequestValidator();
        }

        #endregion

        #region methods

        [Fact]
        public void NotAllowEmptyEmail()
        {
            var customerRequest = new CreateCustomerRequest
            {
                Email = null
            };
            _validators.Validate(customerRequest).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowInvalidEmail()
        {
            var customerRequest = new CreateCustomerRequest
            {
                Email = "dsada"
            };
            _validators.Validate(customerRequest).IsValid.Should().BeFalse();
        }

        [Fact]
        public void AllowValidEmail()
        {
            var customerRequest = new CreateCustomerRequest
            {
                Email = "test@test.com"
            };
            _validators.Validate(customerRequest).IsValid.Should().BeTrue();
        }

        [Fact]
        public void NotAllowRangeFirstNameMinLength()
        {
            var customerRequest = new CreateCustomerRequest
            {
                FirstName = "a"
            };
            _validators.Validate(customerRequest).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowRangeFirstNameMaxLength()
        {
            var customerRequest = new CreateCustomerRequest
            {
                FirstName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            _validators.Validate(customerRequest).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowLastName_When_FirstName_IsNull()
        {
            var customerRequest = new CreateCustomerRequest
            {
                LastName = "aaaa"
            };
            _validators.Validate(customerRequest).IsValid.Should().BeFalse();
        }

        [Fact]
        public void AllowLastName_When_FirstName_HasValue()
        {
            var customerRequest = new CreateCustomerRequest
            {
                FirstName = "sdasda",
                LastName = "aaaa",
                Email = "test@test.com"
            };
            _validators.Validate(customerRequest).IsValid.Should().BeTrue();
        }

        #endregion
    }
}
