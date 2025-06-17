using System.Globalization;

namespace Chat.Domain.Models
{
    public class MessageModel
    {
        public string From { get; set; }
        public int Date { get; set; }
        public string Message { get; set; }
        public string RoomId { get; set; }
        public string To { get; set; }

        public string DateTime
        {
            get
            {
                var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddSeconds(Date).ToLocalTime();
                return dateTime.ToString("M/d/yyyy, h:mm:ss tt", CultureInfo.InvariantCulture);
            }
        }
    }
}
