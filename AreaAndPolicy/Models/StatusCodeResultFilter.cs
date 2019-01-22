using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreaAndPolicy.Models
{
    public class StatusCodeResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // retrieve a typed controller, so we can reuse its data
            if (context.Controller is Controller controller)
            {
                // intercept the NotFoundObjectResult
                if (context.Result is BadRequestObjectResult badRequestObject)
                {
                    // set the model, or other view data
                    controller.ViewData.Model = badRequestObject.Value;

                    // replace the result by a view result
                    context.Result = new ViewResult()
                    {
                        StatusCode = 400,
                        ViewName = "Views/Error/status400.cshtml",
                        ViewData = controller.ViewData,
                        TempData = controller.TempData,
                        
                    };
                }

            }

            await next();
        }
    }
}
