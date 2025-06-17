using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Chat.Domain.Shared.Entity
{
    public class RabbitMqOptions
    {
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Queue { get; set; }
    }
}
