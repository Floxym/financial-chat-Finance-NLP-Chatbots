using Chat.Domain.Interfaces.Services;
using Chat.Domain.Interfaces;
using Chat.Domain.Models.Inputs;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Domain.Hubs
{
    public class ChatHub : Hub
    {
        const string stockMessage = "/stock=";
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStockApiService _stockApiExternalService;

        public ChatHub(IChatService chatService, IUserService userService, IHttpContextAccessor httpContextAccessor, IStockApiService stockApiExternalService)
        {
            _chatService = chatService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _stockApiExternalService = stockApiExternalService;
        }

        public override async Task OnConnectedAsync()
        {


            var roomId = _httpContextAccessor.HttpContext.Request.Query["chatroomId"];
            var userName = _httpContextAccessor.HttpContext?.User.Identity.Name;

            if (userName == null)
                await OnDisconnectedAsync(new Exception("Not Authorized"));
            else
            {
                if (!await VerifyUserAlreadyInRoomAsync(roomId, userName))
                {
                    var user = new UserInput { Username = userName };
                    await _userService.OnStartSession(user, roomId);
                    await Clients.All.SendAsync($"chatroom{roomId}", user.Username);
                }

            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var roomId = _httpContextAccessor.HttpContext.Request.Query["chatroomId"];
            var userName = _httpContextAccessor.HttpContext?.User.Identity.Name;

            if (userName == null)
                await base.OnDisconnectedAsync(exception);
            else
            {
                var user = new UserInput { Username = userName };
                await Clients.All.SendAsync($"chatroom-exit{roomId}", user.Username);
                await _userService.OnStopSession(user, roomId);
            }
        }


        public async Task SendMessage(string message, string roomId, string userTo, bool join, string user = null)
        {
            if (string.IsNullOrEmpty(user))
                user = _httpContextAccessor.HttpContext?.User.Identity.Name;


            if (!string.IsNullOrEmpty(user))
            {
                var date = (int)DateTimeOffset.Now.ToUnixTimeSeconds();
                if (join)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                    await Clients.Group(roomId).SendAsync("ReceiveMessage", user, "joined the toom", date, "all").ConfigureAwait(false);
                }
                else
                {
                    if (!await ProcessStockCodeAsync(message, roomId, user))
                    {
                        var msg = new MessageInput
                        {
                            Date = date,
                            From = user,
                            To = userTo,
                            Message = message,
                            RoomId = roomId
                        };

                        await _chatService.SendMessage(msg);
                        await Clients.Group(roomId).SendAsync("ReceiveMessage", user, message, msg.Date, msg.To).ConfigureAwait(true);
                    }
                }
            }
            else
            {
                await OnDisconnectedAsync(new Exception("Not Authorized"));
            }
        }

        private async Task<bool> ProcessStockCodeAsync(string message, string roomId, string user)
        {
            if (message.Contains(stockMessage))
            {
                var stockCode = message.Substring(message.LastIndexOf("=") + 1, message.Length - message.LastIndexOf("=") - 1);
                var stock = new StockInput { RoomId = roomId, StockCode = stockCode, User = user };
                await _stockApiExternalService.PostAsync(stock);
                return true;
            }

            return false;
        }

        private async Task<bool> VerifyUserAlreadyInRoomAsync(string roomId, string userName)
        {
            var onlineUsers = await _userService.GetOnlineUsersAsync(roomId);
            return onlineUsers.Any(w => w == userName);
        }
    }
}
