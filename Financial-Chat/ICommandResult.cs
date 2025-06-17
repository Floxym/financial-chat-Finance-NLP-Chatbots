using MediatR;

namespace Financial.Chat.Domain.Shared.Commands
{
    public interface ICommandResult<T> : IRequest<T>
    {
        bool IsValid();
    }
}
