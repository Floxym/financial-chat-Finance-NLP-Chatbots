using Chat.Domain.Hubs;
using Chat.Domain.Interfaces;
using Chat.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Chat.Domain.Services
{
    public class NotifyStockService : INotifyStockService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public NotifyStockService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyRoomUserAsync(string code, string userId, string roomId, string value)
        {
            var date = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
            string message;
            if (code.Contains("Error"))
            {
                message = code;
            }
            else
            {
                message = string.Concat(code, " quote is ", value, " per shre");
            }

            await _hubContext.Clients.Group(roomId).SendAsync("ReceiveMessage", userId, message, date, "all");
        }
    }
}
