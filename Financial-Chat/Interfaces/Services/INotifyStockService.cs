using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Interfaces.Services
{
    public interface INotifyStockService
    {
        Task NotifyRoomUserAsync(string code, string userId, string roomId, string value);
    }
}
