using Telegram.Bot;
using TgBot.controller.BotController.Services;

namespace TgBot.LibTgBot.BaseCommand;

public class ThisNotCommand : ICommand
{
    public string Name { get; } = BaseCommand.ExceptionThisNotCommand;
    public string desc { get; }
    public async Task Execute(IRequest? request, ITelegramBotClient bot)
    {
        await bot.SendTextMessageAsync(request.Message.Chat.Id, "command start with '/' ");
    }
}