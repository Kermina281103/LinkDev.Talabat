namespace LinkDev.Talabat.Application.Abstraction.Common.Contracts.Infrastructure
{
    public interface IResponseCacheService
    {
        public Task CacheResponseAsync(string key,object response,TimeSpan timeToLive);

        Task<string?> GetCachedResponseAsync(string key);


      

    }
}
