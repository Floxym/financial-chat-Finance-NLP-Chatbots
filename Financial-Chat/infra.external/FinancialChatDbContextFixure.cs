using Financial.Chat.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Financial.Chat.Tests.ContextDb
{
    public class FinancialChatDbContextFixure
    {
        protected FinancialChatContext db;
        protected static DbContextOptions<FinancialChatContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<FinancialChatContext>();
            builder.UseInMemoryDatabase("FinancialDbTest")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected FinancialChatContext GetDbInstance()
        {
            var options = CreateNewContextOptions();
            return new FinancialChatContext(options);
        }
    }
}
