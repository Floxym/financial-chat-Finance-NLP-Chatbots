using AutoMapper;
using Chat.Domain.Interfaces.Repositories;
using Chat.Domain.Interfaces.Services;
using Chat.Domain.Models;
using Chat.Domain.Models.Inputs;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Chat.Domain.Services
{
    public class ChatService : IChatService
    {
        private readonly IDatabase _database;
        private readonly IMessageService _messageService;
        private readonly IConnectionMultiplexer _redis;
        private readonly IChatRepository _chatRoomRepository;
        private readonly IMapper _mapper;

        public ChatService(IMessageService messageService, IConnectionMultiplexer redis, IChatRepository chatRoomRepository, IMapper mapper)
        {
            _messageService = messageService;
            _redis = redis;
            _database = redis.GetDatabase();
            _chatRoomRepository = chatRoomRepository;
            _mapper = mapper;
        }

        public async Task<List<RoomModel>> GetRoomsAsync() => _mapper.Map<List<RoomModel>>(await _chatRoomRepository.GetAllAsync());

        public async Task<List<MessageModel>> GetMessagesAsync(string roomId, int offset = 0, int size = 50)
        {
            var key = $"room:{roomId}";
            var roomExists = await _database.KeyExistsAsync(key);
            var messages = new List<MessageModel>();

            if (!roomExists)
                return messages;

            var values = await _database.SortedSetRangeByRankAsync(key, offset, offset + size, Order.Descending);

            foreach (var valueRedisVal in values)
            {
                var item = valueRedisVal.ToString();
                if (item is not null)
                    messages.Add(JsonConvert.DeserializeObject<MessageModel>(item));
            }

            return messages.OrderBy(w => w.Date).ToList();
        }

        public async Task SendMessage(MessageInput message)
        {
            await _database.SetAddAsync("online_users", message.From);
            var roomKey = $"room:{message.RoomId}";
            await _database.SortedSetAddAsync(roomKey, JsonConvert.SerializeObject(message), message.Date);
            await _messageService.PublishMessage($"message{message.RoomId}", message);
        }
    }
}
