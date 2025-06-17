using Financial.Chat.Domain.Core.Entity;
using Financial.Chat.Domain.Core.Interfaces;
using Financial.Chat.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace Financial.Chat.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(FinancialChatContext financialChatContext) : base(financialChatContext)
        {
        }

        public void Add(Messages messages)
        {
            Db.Messages.Add(messages);
        }

        public IEnumerable<Messages> GetMessages() => Db.Messages.Take(50);
    }
}
