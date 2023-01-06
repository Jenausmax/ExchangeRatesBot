namespace ExchangeRatesBot.Domain.Models;

/// <summary>
/// Пользователь.
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор чата пользователя.
    /// </summary>
    public long ChatId { get; set; }

    /// <summary>
    /// Никнейм пользователя.
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Флаг подписки.
    /// </summary>
    public bool Subscribe { get; set; }
}
