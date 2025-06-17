using Chat.Domain.Entities;
using Chat.Domain.Models;
using Chat.Domain.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Interfaces.Services
{
    public interface IChatService
    {
        Task<List<RoomModel>> GetRoomsAsync();
        Task<List<MessageModel>> GetMessagesAsync(string roomId = "0", int offset = 0, int size = 50);
        Task SendMessage(MessageInput message);
    }
}
