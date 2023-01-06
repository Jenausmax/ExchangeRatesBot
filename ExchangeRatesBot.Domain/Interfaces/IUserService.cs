using ExchangeRatesBot.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeRatesBot.Domain.Interfaces
{
    public interface IUserService
    {
        CurrentUser CurrentUser { get; set; }

        /// <summary>
        /// Метод установки текущего юзера.
        /// </summary>
        /// <param name="chatId">Чат id юзера.</param>
        /// <param name="user"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> SetUserAsync(long chatId, User user = default, CancellationToken cancel = default);

        /// <summary>
        /// Метод создания юзера.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(User user, CancellationToken cancel);

        /// <summary>
        /// Метод подписки юзера.
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="subscribe"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<bool> SubscribeUpdateAsync(long chatId, bool subscribe, CancellationToken cancel);
    }
}
