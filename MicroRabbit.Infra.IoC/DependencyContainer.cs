using MediatR;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Services;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Data.Repository;
using MicroRabbit.Banking.Domain.CommandHandlers;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Events;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.Bus;
using MicroRabbit.Transfer.Application.Interfaces;
using MicroRabbit.Transfer.Application.Services;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Data.Repository;
using MicroRabbit.Transfer.Domain.EventHandlers;
using MicroRabbit.Transfer.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddSingleton<IEventBus, RabbitMQBus>(sp=>
            {
                var scopeFactory=sp.GetRequiredService<IServiceScopeFactory>(); 
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);   
            }
            );
            service.AddTransient<TransferEventHandler>();
            service.AddTransient<IEventHandler<MicroRabbit.Transfer.Domain.Events.TransferCreatedEvent>, TransferEventHandler>();
            service.AddTransient<IRequestHandler<CreateTransferCommand,bool>,TransferCommandHandler>(); 
            service.AddTransient<IAccountService, AccountService>();
            service.AddTransient<ITransferService, TransferService>();
            service.AddTransient<IAccountRepository, AccountRepository>();
            service.AddTransient<ITransferRepository, TransferRepository>();
            service.AddTransient<BankingDbContext>();
            service.AddTransient<TransferDbContext>();

        }
    }
}
