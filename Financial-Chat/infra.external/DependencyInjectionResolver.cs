using Financial.Chat.Application.Interfaces;
using Financial.Chat.Application.Services;
using Financial.Chat.Domain.Core.CommandHandlers;
using Financial.Chat.Domain.Core.Commands;
using Financial.Chat.Domain.Core.Commands.Message;
using Financial.Chat.Domain.Core.Interfaces;
using Financial.Chat.Domain.Core.Interfaces.Services;
using Financial.Chat.Domain.Shared.Entity;
using Financial.Chat.Domain.Shared.Handler;
using Financial.Chat.Domain.Shared.Notifications;
using Financial.Chat.Infra.Bus.Mediator;
using Financial.Chat.Infra.Data;
using Financial.Chat.Infra.Data.Context;
using Financial.Chat.Infra.Data.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Financial.Chat.Infra.Ioc
{
    public class DependencyInjectionResolver
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddDbContext<FinancialChatContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //MediatR
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRequestHandler<UserAddCommand, bool>, UserHandler>();
            services.AddScoped<IRequestHandler<MessageAddCommand, bool>, UserHandler>();
            services.AddScoped<IRequestHandler<AuthenticateUserCommand, TokenJWT>, LoginHandler>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IQueueMessageService, QueueMessageService>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }
    }
}
