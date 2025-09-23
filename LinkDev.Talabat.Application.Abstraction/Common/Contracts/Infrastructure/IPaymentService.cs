using LinkDev.Talabat.Shared.Models.Basket;


namespace LinkDev.Talabat.Application.Abstraction.Common.Contracts.Infrastructure
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdateIntent(string BasketId);

        public Task UpdateOrderPaymentStatus(string request, string header);
    }
}
