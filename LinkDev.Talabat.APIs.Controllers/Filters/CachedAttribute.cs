using LinkDev.Talabat.Application.Abstraction.Common.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace LinkDev.Talabat.APIs.Controllers.Filters
{
    class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int  _timeToLiveInSeconds;
        public CachedAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var responseCacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var CacheKey = GenereteCacheKeyFromRequest(context.HttpContext.Request);
            var response = await responseCacheService.GetCachedResponseAsync(CacheKey);

            if (!string.IsNullOrEmpty(response)) //Response is already cached
            {
                var result = new ContentResult()
                {
                    Content = response,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = result;
                return;
            }

          var executedActionContext=   await next.Invoke(); //Execute the endpoint 

            if(executedActionContext.Result is OkObjectResult okObjectResult&& okObjectResult is not null)
            {
                await responseCacheService.CacheResponseAsync(CacheKey, okObjectResult.Value,TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }
        }

        private string GenereteCacheKeyFromRequest(HttpRequest request)
        {
            //{{url}}/api/Products?PageIndex=1&PageSize=5&sort=name

            var keyBuilder = new StringBuilder();

            keyBuilder.Append(request.Path);//api/Products 

            //PageIndex = 1
            //PageSize  = 5
            //sort      =name 

            foreach( var (key,value) in request.Query.OrderBy(x=>x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");

                //Key= api/products|PageIndex-1
                //Key= api/products|PageIndex-1|pageSize-5
                //Key= api/products|PageIndex-1|pageSize-5|sort-name

            }
                return keyBuilder.ToString();
        }
    }
  
} 

