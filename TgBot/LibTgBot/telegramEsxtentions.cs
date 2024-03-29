using Telegram.Bot.Types;

namespace TgBot;

public static class telegramExtensions
{
    public static IRequest? Parse(this Message message) //todo: круто было бы обернуть ответ в класс Result. а вовзращать null такое себе. непонятно нифига, если не читать этот код)
    {
        var command = message.Text.Split(' ');
        if (command.Length == 0 || command[0][0] != '/') return null;
        
        var name = command[0];
        var extraData = command.Length == 2 ? command[1] : null;      
        
        return new TgRequest()
        {
            CommandName = name,
            ExtraData = extraData,
            Message = message
        };
    }
}
