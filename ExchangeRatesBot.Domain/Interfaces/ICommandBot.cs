using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ExchangeRatesBot.Domain.Interfaces
{
    public interface ICommandBot
    {
        Task SetUpdateBot(Update update);
        Task SetCommandBot(Telegram.Bot.Types.Enums.UpdateType types);
    }
}
