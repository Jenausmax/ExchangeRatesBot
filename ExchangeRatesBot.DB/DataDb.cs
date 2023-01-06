using ExchangeRatesBot.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesBot.DB;

public class DataDb : DbContext
{
    public DataDb(DbContextOptions<DataDb> options) : base(options)
    {
        
    }

    public DbSet<UserDb> Users { get; set; }
}
