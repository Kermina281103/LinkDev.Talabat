using Azure;
using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Http;
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

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _enviroment.IsDevelopment() ? new ApiExceptionResponse((int)
                    HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace!.ToString())
                    : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message);
                
                
              await  context.Response.WriteAsync(response.ToString());
            }
            

        }
    }



    /// Convention Based MiddleWare => make class EndWith MiddleWare Word then implement in the class mehtod inovke take one par
    /// parameter delegate (next)
    /// Factor =>Make Class then inherit Interface IMiddleware
}
