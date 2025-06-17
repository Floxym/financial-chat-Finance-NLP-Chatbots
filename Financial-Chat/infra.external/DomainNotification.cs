namespace Financial.Chat.Domain.Shared.Notifications
{
    public class DomainNotification : Event.Event
    {
        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
