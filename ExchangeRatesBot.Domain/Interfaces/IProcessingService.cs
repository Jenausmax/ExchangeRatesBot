using ExchangeRatesBot.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeRatesBot.Domain.Interfaces
{
    public interface IProcessingService
    {
        /// <summary>
        /// Метод десериализации и получения данных от Api.
        /// </summary>
        /// <param name="day">Количество дней.</param>
        /// <param name="charCode">Код валюты.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task<Root> RequestProcessingAsync(int day, string charCode, CancellationToken cancellationToken);
    }
}
