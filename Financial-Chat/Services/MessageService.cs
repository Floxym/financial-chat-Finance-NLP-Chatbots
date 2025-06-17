using Chat.Domain.Interfaces.Services;
using Chat.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;

namespace Chat.Domain.Services
{
    public class MessageService : IMessageService
    {
        protected readonly IDatabase _database;
        protected readonly IConnectionMultiplexer _redis;


        public MessageService(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }
        public async Task PublishMessage<T>(string type, T data)
        {
            var jsonData = JsonConvert.SerializeObject(data, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            var pubSubMessage = new PubSubMessageModel()
            {
                Type = type,
                Data = jsonData
            };

            await PublishMessage(pubSubMessage);

        }
        private async Task PublishMessage(PubSubMessageModel pubSubMessage)
        {
            await _database.PublishAsync("MESSAGES", JsonConvert.SerializeObject(pubSubMessage));
        }
    }
}
