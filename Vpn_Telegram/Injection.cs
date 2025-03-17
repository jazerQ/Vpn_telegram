using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Core.Abstractions;
using DataAccess;
using DataAccess.Repositories;
using Infrastructure;
using Infrastructure.VpnLibrary;
using Infrastructure.VpnLibrary.apiRoutes.Get;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using VpnLibrary;
using Weather_bot.Actions;
using Weather_bot.Commands;

namespace Vpn_Telegram
{
    public static class Injection
    {
        public static IServiceProvider GetServiceProvider() 
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IRedisService, RedisService>();
            serviceCollection.AddScoped<ActionByKey>();
            serviceCollection.AddDbContext<TelegramDbContext>(options => options.UseNpgsql(appsettingJsonReader.GetConnectionString()));
            serviceCollection.AddScoped<ITelegramUserRepository, TelegramUserRepository>();
            serviceCollection.AddScoped<ITelegramUserService, TelegramUserService>();
            serviceCollection.AddScoped<BotHandler>();
            serviceCollection.AddHttpClient(StaticInfo.MainPath).ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            }); ;
            serviceCollection.AddScoped<VpnAccessGlobal>();
            serviceCollection.AddScoped<VpnCommand>();
            serviceCollection.AddScoped<IVpnClientRepository, VpnClientRepository>();
            serviceCollection.AddScoped<IVpnClientService, VpnClientService>();
            serviceCollection.AddScoped<IInbounds, Inbounds>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
