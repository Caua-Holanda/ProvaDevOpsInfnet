using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Threading.Tasks;
using ProvaMedGroup.DomainModel.Exceptions;

namespace ProvaMedGroup.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            try
            {
                return _next(httpContext);
            }
            catch (Exception ex)
            {
                string result;
                int code;

                if (ex is TratedExceptions tratedExceptions)
                {
                    code = (int)HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(new
                    {
                        code,
                        message = ex.Message

                    });
                }
                else
                {
                    code = (int)HttpStatusCode.InternalServerError;
                    result = JsonConvert.SerializeObject(new
                    {
                        code,
                        title = "Oops!",
                        message = "Encontramos uma falha ao tentar realizar esta operação no momento.",
                        trace = ex
                    });


                }

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = code;

                return httpContext.Response.WriteAsync(result);
            }


        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
