using System.ComponentModel;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.controller.BotController.Services;
using TgBot.controller.model;

namespace TgBot.Commands;

[Description("Get All Commands")]
public class HelpCommand : ICommand
{
    public string Name { get; } = "/help";
    private readonly List<InfoCommand> _infoCommands;

    public HelpCommand(List<InfoCommand> infoCommands)
    {
        _infoCommands = infoCommands;
    }


    public async Task Execute(IRequest request, ITelegramBotClient bot)
    {
        await bot.SendTextMessageAsync(request.Message.Chat.Id,string.Join("\n",_infoCommands));
    }
}