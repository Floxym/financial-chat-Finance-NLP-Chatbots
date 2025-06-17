namespace Financial.Chat.Domain.Shared.Commands
{
    public interface ICommand 
    {
        bool IsValid();
    }
}
