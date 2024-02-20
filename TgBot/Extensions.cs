using TgBot.Domain.Entity;

namespace TgBot;

public static class Extensions
{
    public static bool Contains(this Mail[] mails, string mailAddress)
    {
        return mails.All(mail => mail.Address != mailAddress);
    }
    
}