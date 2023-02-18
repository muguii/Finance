using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Finance.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        // BEFORE
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.SelectMany(ms => ms.Value.Errors).Select(e => e.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(errors);
            }
        }

        // AFTER
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}
