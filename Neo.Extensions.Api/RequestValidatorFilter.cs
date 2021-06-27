using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Neo.Extensions.Api
{
    public class RequestValidatorFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Method intentionally left empty.
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            var errors = from kvp in context.ModelState
                from e in kvp.Value.Errors
                select new { kvp.Key, e.ErrorMessage };

            context.Result = new BadRequestObjectResult(new Response(notifications: errors));
        }
    }
}
