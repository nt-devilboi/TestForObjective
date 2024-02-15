using Telegram.Bot;
using TgBot.controller.BotController.Services;
using TgBot.controller.model;

namespace TgBot.Commands;

public class HelpCommand : ICommand
{
    public string Name { get; } = "/help";
    public string desc { get; } = "Get All Commands";
    private readonly List<InfoCommand> _infoCommands;

    public HelpCommand(List<InfoCommand> infoCommands)
    {
        _infoCommands = infoCommands;
    }


    public async Task Execute(IRequest? request, ITelegramBotClient bot)
    {
        await bot.SendTextMessageAsync(request.Message.Chat.Id,string.Join("\n",_infoCommands));
    }
}