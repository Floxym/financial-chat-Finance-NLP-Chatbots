using Financial.Chat.Domain.Shared.Entity;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.MessageBroker.Contracts
{
    public interface IFinancialChatApi
    {
        [Post("/user/receive")]
        Task<ApiOkReturn> DeliveryMessage([Body] MessageDto transaction);
    }
}
