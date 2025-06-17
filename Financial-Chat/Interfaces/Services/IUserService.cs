using Chat.Domain.Models;
using Chat.Domain.Models.Inputs;
using Microsoft.AspNetCore.Identity;

namespace Chat.Domain.Interfaces.Services
{
    public interface IUserService
    {
        public Task<IdentityResult> RegisterAsync(UserModel model);
        Task<List<string>> GetOnlineUsersAsync(string roomId, bool excludeMe = false);
        Task OnStartSession(UserInput user, string roomId);
        Task OnStopSession(UserInput user, string roomId);
    }
}
