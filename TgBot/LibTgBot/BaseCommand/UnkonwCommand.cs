using Telegram.Bot;
using TgBot.controller.BotController.Services;

namespace TgBot.LibTgBot.BaseCommand;

public class UnknownCommand : ICommand
{
    public string Name { get; } = "Unknown";
    public string desc { get; }
    public async Task Execute(IRequest request, ITelegramBotClient bot)
    {
        await bot.SendTextMessageAsync(request.Message.Chat.Id, "I don't known this command");
    }
}