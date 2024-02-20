using Microsoft.EntityFrameworkCore;
using TgBot.Domain.Entity;

namespace TgBot.Infrastucture.DataBase;

public class DbObjective : DbContext
{
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Mail> Mails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=127.0.0.1;user=root;password=123123;database=ObjectiveTest", new MySqlServerVersion(new Version(8, 0,0)));
    }
}