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
using Telegram.Bot.Types.Enums;

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

            var botService = scope.GetRequiredService<IBotService>();
            var repo = scope.GetRequiredService<IBaseRepositoryDb<UserDb>>();
            var messageValute = scope.GetRequiredService<IMessageValute>();

            var messageUSD = await messageValute.GetValuteMessage(8, "USD", cancel);
            var messageEUR = await messageValute.GetValuteMessage(8, "EUR", cancel);
            var messageCHY = await messageValute.GetValuteMessage(8, "CNY", cancel);
            var messageGBP = await messageValute.GetValuteMessage(8, "GBP", cancel);
            var messageJPY = await messageValute.GetValuteMessage(8, "JPY", cancel);

            var messagUSD = await messageValute.GetValuteMessage(1, "USD", cancel);
            var messagEUR = await messageValute.GetValuteMessage(1, "EUR", cancel);
            var messagCHY = await messageValute.GetValuteMessage(1, "CNY", cancel);
            var messagGBP = await messageValute.GetValuteMessage(1, "GBP", cancel);
            var messagJPY = await messageValute.GetValuteMessage(1, "JPY", cancel);

            var usersCollectionDb = await repo.GetCollection(cancel);
            var users = usersCollectionDb.Where(u => u.Subscribe == true);

            if (timeNow == timeOne)
            {
                if (users.Any())
                {
                    foreach (var userDb in users)
                    {
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messagUSD, parseMode: ParseMode.Markdown);
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messagEUR, parseMode: ParseMode.Markdown);
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messagGBP, parseMode: ParseMode.Markdown);
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messagCHY, parseMode: ParseMode.Markdown);
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messagJPY, parseMode: ParseMode.Markdown);
                    }
                }

                _logger.Information($"Type: {typeof(JobsSendMessageUsers)}. Task day complete.");
            }

            if (timeNow == timeTwo)
            {
                if (users.Any())
                {
                    foreach (var userDb in users)
                    {
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messageUSD, parseMode: ParseMode.Markdown);
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messageEUR, parseMode: ParseMode.Markdown);
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messageGBP, parseMode: ParseMode.Markdown);
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messageCHY, parseMode: ParseMode.Markdown);
                        await botService.Client.SendTextMessageAsync(userDb.ChatId, messageJPY, parseMode: ParseMode.Markdown);
                    }
                }

                _logger.Information($"Type: {typeof(JobsSendMessageUsers)}. Task night complete.");
            }
        }
    }
}
