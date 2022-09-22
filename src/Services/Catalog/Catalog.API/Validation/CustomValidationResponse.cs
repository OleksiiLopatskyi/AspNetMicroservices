using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Validation
{
    public class CustomValidationResponse
    {
        public static IActionResult CreateResponse(ActionContext context)
        {
            var result = new BadRequestObjectResult(context.ModelState);
            return result;
        }
    }
}
