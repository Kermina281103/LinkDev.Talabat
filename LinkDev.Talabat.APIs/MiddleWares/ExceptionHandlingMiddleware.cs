using Azure;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace LinkDev.Talabat.APIs.MiddleWares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _enviroment;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger,IWebHostEnvironment webHostEnvironment)
        {
           _next = next;
            _logger = logger;
            _enviroment = webHostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //Logic will be exectuted for the reques 
               await _next(context);//Go to next middleware or the application itself
               /// if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
               /// {
               ///     var response = new ApiResponse((int)HttpStatusCode.NotFound, $"the requested endpoing :{context.Request.Path} is not found ");
               ///     await context.Response.WriteAsync(response.ToString());
               /// }
                // Logic with will be exectured for the response 
            }
            catch (Exception ex)
            {

                #region Logging :TODO With Serial Package 
                if (_enviroment.IsDevelopment())
                {
                    _logger.LogError(ex, ex.Message, ex.StackTrace!.ToString());
                }
                else
                {
                    //Log Exception in Extrenal Resource like Database or File 

                }
                #endregion

                await HandleExceptionsAsync(context, ex);
            }


        }

        private async Task HandleExceptionsAsync(HttpContext context, Exception ex)
        {
            ApiResponse response;
            switch (ex)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new ApiResponse( (int)HttpStatusCode.NotFound, ex.Message);
                    break;
                case ValidationExceptions validationExceptions:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ApiValidationErrorResponse(ex.Message)
                    {
                        Errors = (IEnumerable<ApiValidationErrorResponse.ValidationError>)validationExceptions.Errors
                    };
                    break;
                case BadRequesException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ApiResponse((int)HttpStatusCode.BadRequest, ex.Message);
                    break;
                case UnAuthorizedExceptoin:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = new ApiResponse((int)HttpStatusCode.Unauthorized, ex.Message);
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = _enviroment.IsDevelopment() ? new ApiExceptionResponse((int)
                     HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace!.ToString())
                    : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message);
                    break;
            }


            context.Response.ContentType = "application/json";

         


            await context.Response.WriteAsync(response.ToString());
        }
    }



    /// Convention Based MiddleWare => make class EndWith MiddleWare Word then implement in the class mehtod inovke take one par
    /// parameter delegate (next)
    /// Factor =>Make Class then inherit Interface IMiddleware
}
