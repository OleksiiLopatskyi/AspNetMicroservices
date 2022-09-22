using Catalog.API.DTO.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.API.Middleware
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                var result = string.Empty;
                response.ContentType = "application/json";
                var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                switch (ex)
                {
                    case KeyNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        result = JsonSerializer.Serialize(new ErrorResponse(ex.Message), jsonOptions);
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                await response.WriteAsync(result);
            }
        }
    }
}