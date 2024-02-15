using Telegram.Bot.Types;

namespace TgBot;

public class TgRequest : IRequest
{
    public string CommandName { get; set; }
    public string ExtraData { get; set; }
    public Message Message { get; set; }
    
    public bool IsCommand()
    {
        return CommandName != "ThisNotCommand";
    }
    
    
}