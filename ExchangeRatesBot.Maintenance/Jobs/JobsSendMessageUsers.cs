using ExchangeRatesBot.Configuration.ModelConfig;
using ExchangeRatesBot.Maintenance.Abstractions;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExchangeRatesBot.DB.Models;
using ExchangeRatesBot.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRatesBot.Maintenance.Jobs
{
    public class JobsSendMessageUsers : BackgroundTaskAbstract<JobsSendMessageUsers>
    {
        private readonly IOptions<BotConfig> _config;
        private readonly ILogger _logger;

        public JobsSendMessageUsers(IServiceProvider services, IOptions<BotConfig> config, ILogger logger) 
            : base(services, config, logger)
        {
            _config = config;
            _logger = logger;
        }

        protected override async Task DoWorkAsync(CancellationToken cancel, IServiceProvider scope)
        {
            DateTime timeOne;
            DateTime timeTwo;
            DateTime timeNow;

            var cur = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            DateTime.TryParseExact(cur, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out timeNow);
            DateTime.TryParse(_config.Value.TimeOne, out timeOne);
            DateTime.TryParse(_config.Value.TimeTwo, out timeTwo);

            _logger.Information("Задача работает.");

            if (timeNow == timeOne || timeNow == timeTwo)
            {
                _logger.Information("Начата.");
                var botService = scope.GetRequiredService<IBotService>();
                var repo = scope.GetRequiredService<IBaseRepositoryDb<UserDb>>();
                var messageValute = scope.GetRequiredService<IMessageValute>();
                //USD, EUR, CNY, GBP, JPY
                var messageUSD = await messageValute.GetValuteMessage(8, "USD", cancel);
                var messageEUR = await messageValute.GetValuteMessage(8, "EUR", cancel);
                var messageCHY = await messageValute.GetValuteMessage(8, "CNY", cancel);
                var messageGBP = await messageValute.GetValuteMessage(8, "GBP", cancel);
                var messageJPY = await messageValute.GetValuteMessage(8, "JPY", cancel);

                var usersCollectionDb = await repo.GetCollection(cancel);
                var users = usersCollectionDb.Where(u => u.Subscribe == true);

                if (users.Any())
                {
                    foreach (var userDb in users)
                    {
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messageUSD);
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messageEUR);
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messageGBP);
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messageCHY);
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messageJPY);
                    }
                }

                _logger.Information($"Type: {typeof(JobsSendMessageUsers)}. Task complite.");
            }
        }
    }
}
