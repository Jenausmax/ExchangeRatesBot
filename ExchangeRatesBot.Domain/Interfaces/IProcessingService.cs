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
        /// <summary>
        /// Метод десериализации и получения данных от Api.
        /// </summary>
        /// <param name="day">Количество дней.</param>
        /// <param name="charCode">Код валюты.</param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<Root> RequestProcessing(int day, string charCode, CancellationToken cancel);
    }
}
