using ExchangeRatesBot.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ExchangeRatesBot.App.Services
{
    public class CommandService : ICommandBot
    {
        private readonly IUpdateService _updateService;
        private readonly IProcessingService _processingService;
        private Update _update;

        public CommandService(IUpdateService updateService, IProcessingService processingService)
        {
            _updateService = updateService;
            _processingService = processingService;
        }

        public async Task SetUpdateBot(Update update)
        {
            _update = update;
        }


        public async Task SetCommandBot(Telegram.Bot.Types.Enums.UpdateType type)
        {
            switch (type)
            {
                case UpdateType.Message:
                    await MessageCommand(_update);
                    break;

                case UpdateType.CallbackQuery:
                    
                    break;

                default:
                    //TODO Доделать ответ на неправильное сообщение от пользователя
                    break;
            }
        }

        private async Task MessageCommand(Update update)
        {
            var message = update.Message.Text;
            switch (message)
            {
                case "/start":

                    var val = await _processingService.RequestProcessing(4, "USD", CancellationToken.None);
                    //await _updateService.EchoTextMessageAsync(
                    //    update,
                    //    BotPhrases.Start,
                    //    _keyboardBotCreate.CreateInlineKeyboard(
                    //        callBack: default,
                    //        key: default,
                    //        keyCollection: BotPhrases.AllCommandMenu()));

                    if (val == null)
                    {

                    }
                    break;


                default:
                    break;
            }
        }
    }
}
