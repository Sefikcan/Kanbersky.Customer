using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Kanbersky.Customer.Core.Validators
{
    public class ResponseValidator : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new List<string>();

                foreach (var modelState in context.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                throw new ValidatorException(errors);
            }
        }
    }
}
