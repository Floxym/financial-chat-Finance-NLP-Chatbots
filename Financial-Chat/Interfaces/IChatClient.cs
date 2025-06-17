using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Interfaces
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message, int date, string to);
    }
}
