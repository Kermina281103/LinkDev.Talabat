using LinkDev.Talabat.Shared.Models.Basket;


namespace LinkDev.Talabat.Domain.Contract.Infrastructure
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdateIntent(string BasketId);    
    }
}
