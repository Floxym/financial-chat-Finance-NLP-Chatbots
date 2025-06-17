using Chat.Domain.Entities;
using Chat.Domain.Interfaces.Repositories;
using Chat.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infra.Data.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatContext _context;

        public ChatRepository(ChatContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Room>> GetAllAsync() => await _context.Rooms.ToListAsync();
    }
}
