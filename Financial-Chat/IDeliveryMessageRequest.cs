using Financial.Chat.Domain.Shared.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.MessageBroker.Contracts
{
    public interface IDeliveryMessageRequest
    {
        Task<ApiOkReturn> DeliveryMessageAsync(MessageDto message);
    }
}
