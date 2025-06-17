using Financial.Chat.Domain.Shared.Commands;
using Financial.Chat.Domain.Shared.Handler;
using MediatR;
using System.Threading.Tasks;

namespace Financial.Chat.Infra.Bus.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : class
        {
            return _mediator.Publish(@event);
        }

        public async Task<TResult> SendCommandResult<TResult>(ICommandResult<TResult> command)
        {
            return await _mediator.Send(command);
        }
    }
}
