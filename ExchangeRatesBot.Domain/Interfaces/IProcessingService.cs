using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExchangeRatesBot.Domain.Models;

namespace ExchangeRatesBot.Domain.Interfaces
{
    public interface IProcessingService
    {
        Task<Valute> RequestProcessing(int day, string charCode, CancellationToken cancel);
    }
}
