using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PZCheeseria.BusinessLogic.Exceptions;

namespace PZCheeseria.Api.Infrastructure.Middleware
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private JsonSerializerSettings _jsonSettings;

        public ApiExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            _jsonSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        /// <remarks>
        ///Currently only interpreting UnProcessableEntityException and EntityNotFoundException (for GET method only)
        /// we can extend it to interpret combination of request type and exception and return appropriate status code
        /// for example, if the exception caught is EntityNotFoundException and request type is Get then we can return NotFound
        /// but if request type is Post, we can return BadRequest with appropriate details
        /// </remarks>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted) //we cannot do much if response has already started
                {
                    throw;
                }

                if (ex.GetType() == typeof(UnProcessableEntityException))
                {
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    var unprocessableException = (UnProcessableEntityException) ex;
                    var apiExceptionDetails = new ApiExceptionDetails();
                    apiExceptionDetails.Errors = unprocessableException.Errors.Select(m => m);
                    apiExceptionDetails.ModelStateErrors = unprocessableException.ModelStateErrors.Select(m => new ModelStateError
                    {
                        Error = m.Error,
                        PropertyName = m.PropertyName
                    });

                    var json = JsonConvert.SerializeObject(apiExceptionDetails, _jsonSettings);
                    await context.Response.WriteAsync(json);
                    return;
                }
                if (ex.GetType() == typeof(EntityNotFoundException) && context.Request.Method == HttpMethods.Get)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    return;
                }

                throw;
            }
        }
    }
    
    public class ApiExceptionDetails
    {
        public ApiExceptionDetails()
        {
            Errors = Enumerable.Empty<string>();
            ModelStateErrors = Enumerable.Empty<ModelStateError>();
        }

        public IEnumerable<string> Errors { get; set; }
        public IEnumerable<ModelStateError> ModelStateErrors { get; set; }
    }

    public class ModelStateError
    {
        public string PropertyName { get; set; }

        public string Error { get; set; }
    }
}