
namespace Chat.Domain.Models.Inputs
{
    public class UserInput
    {
        public string Username { get; set; }
        public bool IsOnline { get; set; } = false;
    }
}
