using System.Collections.Generic;
using ExchangeRatesBot.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using ExchangeRatesBot.App.Phrases;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ExchangeRatesBot.App.Services
{
    public class CommandService : ICommandBot
    {
        private readonly IUpdateService _updateService;
        private readonly IMessageValute _valuteService;
        private Update _update;

        public CommandService(IUpdateService updateService, 
            IProcessingService processingService,
            IMessageValute valuteService)
        {
            _updateService = updateService;
            _valuteService = valuteService;
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
                    await CallbackMessageCommand(_update);
                    break;

                default:
                    //TODO Доделать ответ на неправильное сообщение от пользователя
                    break;
            }
        }

        private async Task CallbackMessageCommand(Update update)
        {
            var callbackData = update.CallbackQuery.Data;
            switch (callbackData)
            {
                case "7 Day":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        await _valuteService.GetValuteMessage(7, "USD", CancellationToken.None),//BotPhrases.Help,
                        default);
                    break;

                case "Подписаться":
                    break;

                case "Отписаться":
                    break;
            }
        }

        private async Task MessageCommand(Update update)
        {
            var message = update.Message.Text;
            switch (message)
            {
                case "/start":
                    await _updateService.EchoTextMessageAsync(
                        update,
                        await _valuteService.GetValuteMessage(8, "USD", CancellationToken.None),
                        default);
                    await _updateService.EchoTextMessageAsync(
                        update,
                        await _valuteService.GetValuteMessage(8, "EUR", CancellationToken.None),
                        default);

                    await _updateService.EchoTextMessageAsync(
                        update,
                        BotPhrases.StartMenu,
                        new InlineKeyboardMarkup(Menu()));
                    break;


                default:
                    break;
            }
        }

        private List<InlineKeyboardButton> Menu()
        {
            var button = new InlineKeyboardButton();
            button.Text = "Подписаться";
            var button2 = new InlineKeyboardButton();
            button2.Text = "Отписаться";
            var buttons = new List<InlineKeyboardButton>();
            buttons.Add(button);
            buttons.Add(button2);
            return buttons;
        }
    }
}
