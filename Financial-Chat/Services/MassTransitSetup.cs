using Microsoft.Extensions.DependencyInjection;
using System;
using MassTransit;
using GreenPipes;
using Financial.Chat.Domain.Shared.Entity;

namespace Financial.Chat.Web.API.Config
{
    public static class MassTransitSetup
    {
        public static void AddMassTransitSetup(this IServiceCollection services, RabbitMqOptions options)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(options.Url), h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });
            });

            services.AddSingleton(busControl);
        }
    }
}
