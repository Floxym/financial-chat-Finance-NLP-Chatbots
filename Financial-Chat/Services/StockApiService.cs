using Chat.Domain.Interfaces.Services;
using Chat.Domain.Models.Inputs;
using Domain.Interfaces.ExternalServices;

namespace Chat.Domain.Services
{
    public class StockApiService : IStockApiService
    {
        public readonly IStockApiExternalService _stockApiExternalService;

        public StockApiService(IStockApiExternalService stockApiExternalService)
        {
            _stockApiExternalService = stockApiExternalService;
        }

        public async Task PostAsync(StockInput model)
        {
            await _stockApiExternalService.PostAsync(model);
        }
    }
}
