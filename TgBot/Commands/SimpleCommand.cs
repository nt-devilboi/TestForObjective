using System.ComponentModel;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Quic;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.controller.BotController.Services;
using TgBot.controller.model;
using Vostok.Logging.Abstractions;

namespace TgBot.Commands;

[Description("запуск бота")]
public class SimpleCommand : ICommand
{
    public string Name { get; } = "/start";

    public async Task Execute(IRequest request, ITelegramBotClient bot)
    {
        var chatId = request.Message.Chat.Id;
        await bot.SendTextMessageAsync(chatId, "привеееееееет");
    }
}