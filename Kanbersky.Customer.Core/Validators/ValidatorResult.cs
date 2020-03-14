using Kanbersky.Customer.Core.Results;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Kanbersky.Customer.Core.Validators
{
    public class ValidatorResult : Result
    {
        public List<string> Errors { get; set; }

        public ValidatorResult(List<string> errors) : base(false, StatusCodes.Status400BadRequest)
        {
            Errors = errors;
        }
    }
}
