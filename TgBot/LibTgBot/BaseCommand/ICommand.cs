using Telegram.Bot;
using Message = Telegram.Bot.Types.Message;

namespace TgBot.controller.BotController.Services;

public interface ICommand
{
    public string Name { get; }
    
    public string desc { get; }
    public Task Execute(IRequest request, ITelegramBotClient bot);
}