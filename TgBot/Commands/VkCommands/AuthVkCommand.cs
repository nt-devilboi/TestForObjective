using System.ComponentModel;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot.controller.BotController.Services;
using UlearnTodoTimer.Infrasturcture.Services.AppAuth;

namespace TgBot.Commands;

[Description("Auth By vk")]
public class AuthVk : ICommand
{
   
    public string Name { get; } = "/vk";
    public string desc { get; } = "auth by vk";
    
    private readonly IProvideOAuth _authSettings;
    private readonly string _text = "Vk";
    
    public AuthVk(IProvideOAuth authSettings)
    {
        _authSettings = authSettings;
    }
    
    public async Task Execute(IRequest? request, ITelegramBotClient bot)
    {
        var vkOAuth = _authSettings.GetOAuth("vk");
        var chatId = request.Message.Chat.Id;
        var bottons = new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(_text, vkOAuth.CreateAuthRequest(chatId.ToString())));
        await bot.SendTextMessageAsync(request.Message.Chat.Id, "выбери где авторизоваться", parseMode: ParseMode.Html,
            replyMarkup: bottons);
    }
}

/*
  private readonly string _authUrlStandalone =
      "https://oauth.vk.com/authorize?client_id=51749665&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=wall,friends&response_type=token&v=5.131";
      */
