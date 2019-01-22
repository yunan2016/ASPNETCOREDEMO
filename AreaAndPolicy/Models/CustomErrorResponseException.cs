using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AreaAndPolicy.Models
{
    // Custom exceptions that can be thrown within the middleware
    public class CustomErrorResponseException : Exception
    {
        public int StatusCode { get; set; }
        public CustomErrorResponseException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }

    public class NotFoundResponseException : CustomErrorResponseException
    {
        public NotFoundResponseException(string message)
            : base(message, 404)
        { }
    }

    // Custom context feature, to store information from the exception
    public interface ICustomErrorResponseFeature
    {
        int StatusCode { get; set; }
        string StatusMessage { get; set; }
    }
    public class CustomErrorResponseFeature : ICustomErrorResponseFeature
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }

    // Middleware implementation
    public class CustomErrorResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _requestPath;

        public CustomErrorResponseMiddleware(RequestDelegate next, string requestPath)
        {
            _next = next;
            _requestPath = requestPath;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // run the pipeline normally
                await _next(context);

                StreamReader reader = new StreamReader(context.Response.Body);
                    string text = reader.ReadToEnd();


                context.Features.Set<ICustomErrorResponseFeature>(new CustomErrorResponseFeature
                {
                    StatusCode = context.Response.StatusCode,

                   

                StatusMessage = text,
                });

                // backup original request data
                var originalPath = context.Request.Path;
                var originalQueryString = context.Request.QueryString;

                // set new request data for re-execution
                context.Request.Path = _requestPath;
                context.Request.QueryString = QueryString.Empty;

                try
                {
                    // re-execute middleware pipeline
                    await _next(context);
                }
                finally
                {
                    // restore original request data
                    context.Request.Path = originalPath;
                    context.Request.QueryString = originalQueryString;
                }

            }
            catch (CustomErrorResponseException ex)
            {
                // store error information to be retrieved in the custom handler
                context.Features.Set<ICustomErrorResponseFeature>(new CustomErrorResponseFeature
                {
                    StatusCode = ex.StatusCode,
                    StatusMessage = ex.Message,
                });

                // backup original request data
                var originalPath = context.Request.Path;
                var originalQueryString = context.Request.QueryString;

                // set new request data for re-execution
                context.Request.Path = _requestPath;
                context.Request.QueryString = QueryString.Empty;

                try
                {
                    // re-execute middleware pipeline
                    await _next(context);
                }
                finally
                {
                    // restore original request data
                    context.Request.Path = originalPath;
                    context.Request.QueryString = originalQueryString;
                }
            }
        }
    }
}
