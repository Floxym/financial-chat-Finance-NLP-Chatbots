using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Interfaces.Services
{
    public interface IMessageService
    {
        Task PublishMessage<T>(string type, T data);
    }
}
