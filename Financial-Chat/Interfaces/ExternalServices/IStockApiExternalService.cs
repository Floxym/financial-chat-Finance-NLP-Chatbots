using Chat.Domain.Models.Inputs;

namespace Domain.Interfaces.ExternalServices
{
    public interface IStockApiExternalService
    {
        Task PostAsync(StockInput model);
    }
}
