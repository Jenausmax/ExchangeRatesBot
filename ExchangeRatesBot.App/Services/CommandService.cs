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
        private readonly IUserService _userControl;
        private Update _update;

        public CommandService(IUpdateService updateService, 
            IProcessingService processingService,
            IMessageValute valuteService,
            IUserService userControl)
        {
            _updateService = updateService;
            _valuteService = valuteService;
            _userControl = userControl;
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

                    var resMessageUser = await _userControl.SetUser(_update.Message.From.Id);
                    if (resMessageUser == false)
                    {
                        var user = new Domain.Models.User()
                        {
                            ChatId = _update.Message.From.Id,
                            NickName = _update.Message.From.Username,
                            Subscribe = false,
                            FirstName = _update.Message.From.FirstName,
                            LastName = _update.Message.From.LastName
                        };
                        await _userControl.Create(user, CancellationToken.None);
                        await _userControl.SetUser(user.ChatId);
                    }

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
