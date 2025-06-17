using AutoMapper;
using Chat.Domain.Interfaces.Services;
using Chat.Domain.Models;
using Chat.Domain.Models.Inputs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;

namespace Chat.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IDatabase _database;
        private readonly IMessageService _messageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserService(IConnectionMultiplexer redis, IMessageService messageService, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _database = redis.GetDatabase();
            _messageService = messageService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(UserModel model)
        {
            var user = _mapper.Map<IdentityUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                await _signInManager.SignInAsync(user, isPersistent: false);
            return result;
        }

        public async Task<List<string>> GetOnlineUsersAsync(string roomId, bool excludeMe = false)
        {
            var userName = _httpContextAccessor.HttpContext?.User.Identity.Name;

            var key = $"room:{roomId}:online_users";
            var roomExists = await _database.KeyExistsAsync(key);
            var users = new List<string>();

            if (!roomExists)
            {
                return users;
            }
            var values = await _database.SetMembersAsync(key);

            foreach (var valueRedisVal in values)
            {
                var value = valueRedisVal.ToString();
                users.Add(value);
            }

            if (excludeMe)
                users.Remove(userName);
            return users;
        }


        public async Task OnStartSession(UserInput user, string roomId)
        {
            await _database.SetAddAsync($"room:{roomId}:online_users", user.Username);
            user.IsOnline = true;
            await _messageService.PublishMessage("user.connected", user);
        }

        public async Task OnStopSession(UserInput user, string roomId)
        {
            await _database.SetRemoveAsync($"room:{roomId}:online_users", user.Username);
            user.IsOnline = false;
            await _messageService.PublishMessage("user.disconnected", user);
        }
    }
}
