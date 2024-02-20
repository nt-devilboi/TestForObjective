using System.Collections.Concurrent;
using MyBotTg.Bot;
using TgBot.Domain.Entity;

namespace TgBot;

public class MailRepositoryFake : IMailRepository
{
    private readonly ConcurrentDictionary<long, Mail> _concurrentDictionary =
        new ConcurrentDictionary<long, Mail>();

    public async Task Add(Mail mail)
    {
        _concurrentDictionary.TryAdd(mail.ChatId, mail);
    }
    
    public async Task<Mail[]> GetAllMailByChatId(long chatId)
    {
        if (_concurrentDictionary.TryGetValue(chatId, out var email))
        {
            return new[] { email };
        }

        return Array.Empty<Mail>();
    }

    public Task Remove(Mail mail)
    {
        throw new NotImplementedException();
    }
}