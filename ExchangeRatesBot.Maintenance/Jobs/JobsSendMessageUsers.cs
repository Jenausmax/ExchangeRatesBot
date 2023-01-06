using ExchangeRatesBot.App.Phrases;
using ExchangeRatesBot.Configuration.ModelConfig;
using ExchangeRatesBot.DB.Models;
using ExchangeRatesBot.Domain.Interfaces;
using ExchangeRatesBot.Maintenance.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using static ExchangeRatesBot.Maintenance.Jobs.JobConstants;

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

            if (timeNow == timeOne)
            {
                await TaskListUsersAsync(scope, cancel);

                _logger.Information(Day);
            }

            if (timeNow == timeTwo)
            {
                await TaskListUsersAsync(scope, cancel);

                _logger.Information(Night);
            }
        }

        private async Task TaskListUsersAsync(IServiceProvider scope, CancellationToken cancellationToken)
        {
            var botService = scope.GetRequiredService<IBotService>();
            var messageValute = scope.GetRequiredService<IMessageValute>();
            var repo = scope.GetRequiredService<IBaseRepositoryDb<UserDb>>();
            var usersCollectionDb = await repo.GetCollection(cancellationToken);

            var users = usersCollectionDb.Where(u => u.Subscribe == true).ToArray();

            var message = await messageValute.GetValuteMessage(1, BotPhrases.Valutes, cancellationToken);

            if (users.Any())
            {
                foreach (var userDb in users)
                {
                    await botService.Client.SendTextMessageAsync(userDb.ChatId, message, parseMode: ParseMode.Markdown, cancellationToken: cancellationToken);
                }
            }
        }
    }
}
