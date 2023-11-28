using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.controller.BotController.Services;

namespace TgBot.Commands;

[Description("Auth By vk")]
public class AuthVk : ICommand
{
    public string Name { get; } = "/vk";
    public string desc { get; } = "auth by vk";
    private readonly string _text = "Vk";

    private string _authUrlWebApp(long chatId) =>
        $"https://oauth.vk.com/authorize?client_id=51749903&state={chatId}&display=page&scope=wall&redirect_uri=http://localhost:5128/OAuth/Bot&response_type=code&v=5.131";
    
    private readonly string _authUrlStandalone = 
        "https://oauth.vk.com/authorize?client_id=51749665&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=wall,friends&response_type=token&v=5.131";


    public async Task Execute(IRequest request, ITelegramBotClient bot)
    {
        var chatId = request.Message.Chat.Id;
        var bottons = new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(_text, _authUrlWebApp(chatId)));
        using var clinet = new HttpClient();
        await bot.SendTextMessageAsync(request.Message.Chat.Id, "выбери где авторизоваться", parseMode: ParseMode.Html,
            replyMarkup: bottons);
    }
}