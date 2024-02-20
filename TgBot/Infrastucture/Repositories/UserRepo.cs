using Microsoft.EntityFrameworkCore;
using MyBotTg.Bot;
using TgBot.Domain.Entity;
using TgBot.Infrastucture.DataBase;

namespace TgBot;

public class MailRepo : IMailRepository 
{
    private readonly DbObjective _db;

    public MailRepo(DbObjective db)
    {
        _db = db;
    }
    
    public async Task Add(Mail mail)
    {
        await _db.Mails.AddAsync(mail);
        await _db.SaveChangesAsync();
    }

    public async Task<Mail[]> GetAllMailByChatId(long chatId)
    {
        return await _db.Mails.Where(x => x.ChatId == chatId).ToArrayAsync();
    }

    // реализизация есть, но по Т3 такой фичи нету.
    public async Task Remove(Mail mail)
    {
        _db.Mails.Remove(mail);
        await _db.SaveChangesAsync();
    }
}