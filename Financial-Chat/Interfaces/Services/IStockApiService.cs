using Chat.Domain.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Interfaces.Services
{
    public interface IStockApiService
    {
        Task PostAsync(StockInput model);
    }
}
