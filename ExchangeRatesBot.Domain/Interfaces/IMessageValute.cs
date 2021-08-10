using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeRatesBot.Domain.Interfaces
{
    public interface IMessageValute
    {
        Task<string> GetValuteMessage(int day, string charCode, CancellationToken cancel);
        Task<string> GetValuteMessage(int day, string[] charCodesCollection, CancellationToken cancel);
    }
}
