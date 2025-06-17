using Financial.Chat.Domain.Shared.Commands;
using System.Threading.Tasks;

namespace Financial.Chat.Domain.Shared.Handler
{
    public interface IMediatorHandler
    {
        Task<TResult> SendCommandResult<TResult>(ICommandResult<TResult> command);
        Task RaiseEvent<T>(T @event) where T : class;
    }
}
