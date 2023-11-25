using System.Diagnostics.CodeAnalysis;
using System.Resources;
using Telegram.Bot.Types;
using TgBot.controller.model;

namespace TgBot;

public static class telegramExtensions
{
    public static IRequest Parse(this Message message)
    {
        var command = message.Text.Split(' ');
        if (command.Length == 0 || command[0][0] != '/')
        {
            return new Request()
            {
                CommandName = "ThisNotComman",
                ExtraData = null,
                Message = message
            };
        };
        
        var name = command[0];
        var extraData = command.Length == 2 ? command[1] : null;      
        
        return new Request()
        {
            CommandName = name,
            ExtraData = extraData,
            Message = message
        };
    }
    
    
    
}

public interface IRequest
{
    public string CommandName { get; set; }
    public string ExtraData { get; set; }
    
    public Message Message { get; set; }

    public bool IsCommand();
}

class Request : IRequest
{
    public string CommandName { get; set; }
    public string ExtraData { get; set; }
    public Message Message { get; set; }

    
    public bool IsCommand()
    {
        return CommandName != "ThisNotCommand";
    }
}