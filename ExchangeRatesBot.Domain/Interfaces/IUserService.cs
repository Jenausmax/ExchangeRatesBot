using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExchangeRatesBot.Domain.Models;

namespace ExchangeRatesBot.Domain.Interfaces
{
    public interface IUserService
    {
        Task<bool> SetUser(long chatId, User user = default, CancellationToken cancel = default);
        Task<bool> Create(User user, CancellationToken cancel);
        Task<bool> SubscribeUpdate(int chatId, bool subscribe, CancellationToken cancel);
    }
}
