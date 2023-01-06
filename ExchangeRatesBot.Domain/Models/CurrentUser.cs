namespace ExchangeRatesBot.Domain.Models;

/// <summary>
/// Текущий пользователь.
/// </summary>
public class CurrentUser
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор чата пользователя.
    /// </summary>
    public long ChatId { get; set; }

    /// <summary>
    /// Никнейм пользователя.
    /// </summary>
    public string NickName { get; set; }
}