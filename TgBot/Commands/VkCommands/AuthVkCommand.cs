using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.controller.BotController.Services;

namespace TgBot.Commands;

[Description("Auth By vk")]
public class AuthVkCommand : ICommand
{
    public string Name { get; } = "/vk";
    private readonly string _text = "Vk";

    private readonly string _authUrlWebApp =
        "https://oauth.vk.com/authorize?client_id=51749903&display=page&scope=wall&redirect_uri=http://localhost:5128/OAuth/Bot&response_type=code&v=5.131";
    
    private readonly string _authUrlStandalone = 
        "https://oauth.vk.com/authorize?client_id=51749665&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=wall,friends&response_type=token&v=5.131";


    public async Task Execute(IRequest request, ITelegramBotClient bot)
    {
        var bottons = new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(_text, _authUrlStandalone));
        using var clinet = new HttpClient();
        await bot.SendTextMessageAsync(request.Message.Chat.Id, "выбери где авторизоваться", parseMode: ParseMode.Html,
            replyMarkup: bottons);
    }
}