using Telegram.Bot;
using TgBot.controller.BotController.Services;

namespace TgBot.LibTgBot.BaseCommand;

public class ThisNotCommand : ICommand
{
    public string Name { get; } = BaseCommand.ExceptionThisNotCommand;
    public string desc { get; }
    public async Task Execute(IRequest? request, ITelegramBotClient bot)
    {
        var x = request.Message.Chat.Id;
        await bot.SendTextMessageAsync(x, "command start with '/' ");
    }
}