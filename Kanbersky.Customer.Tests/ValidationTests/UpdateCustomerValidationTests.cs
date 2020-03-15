using FluentAssertions;
using Kanbersky.Customer.Business.DTO.Request;
using Kanbersky.Customer.Business.Validators;
using Xunit;

namespace Kanbersky.Customer.Tests.ValidationTests
{
    public class UpdateCustomerValidationTests
    {
        #region fields

        private UpdateCustomerRequestValidator _validators;

        #endregion

        #region ctor

        public UpdateCustomerValidationTests()
        {
            _validators = new UpdateCustomerRequestValidator();
        }

        #endregion

        #region methods

        [Fact]
        public void NotAllowEmptyEmail()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                Email = null
            };
            _validators.Validate(updateRequest).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowInvalidEmail()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                Email = "dsada"
            };
            _validators.Validate(updateRequest).IsValid.Should().BeFalse();
        }

        [Fact]
        public void AllowValidEmail()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                Id =1,
                Email = "test@test.com"
            };
            _validators.Validate(updateRequest).IsValid.Should().BeTrue();
        }

        [Fact]
        public void NotAllowRangeFirstNameMinLength()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                FirstName = "a"
            };
            _validators.Validate(updateRequest).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowRangeFirstNameMaxLength()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                FirstName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            _validators.Validate(updateRequest).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowLastName_When_FirstName_IsNull()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                LastName = "aaaa"
            };
            _validators.Validate(updateRequest).IsValid.Should().BeFalse();
        }

        [Fact]
        public void AllowLastName_When_FirstName_HasValue()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                Id = 1,
                FirstName = "sdasda",
                LastName = "aaaa",
                Email = "test@test.com"
            };
            _validators.Validate(updateRequest).IsValid.Should().BeTrue();
        }

        [Fact]
        public void NotAllowIdEqualZero()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                Id = 0
            };
            _validators.Validate(updateRequest).IsValid.Should().BeFalse();
        }

        [Fact]
        public void AllowIdGreaterThanZero()
        {
            var updateRequest = new UpdateCustomerRequest
            {
                Id = 1
            };
            _validators.Validate(updateRequest).IsValid.Should().BeFalse();
        }

        #endregion
    }
}
