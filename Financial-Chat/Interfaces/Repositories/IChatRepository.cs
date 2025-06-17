using Chat.Domain.Entities;

namespace Chat.Domain.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
    }
}
