using System.ComponentModel.DataAnnotations;

namespace ExchangeRatesBot.Domain.Models;

/// <summary>
/// Абстрактная сущность.
/// </summary>
public class Entity
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    [Key]
    public int Id { get; set; }
}
