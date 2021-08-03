using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeRatesBot.Domain.Interfaces
{
    public interface IMessageValute
    {
        Task<string> GetValuteMessage(int day, string charCode, CancellationToken cancel);
    }
}
