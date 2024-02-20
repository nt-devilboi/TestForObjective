using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.controller.model;
using TgBot.LibTgBot.BaseCommand;

namespace TgBot.controller.BotController;

[ApiController]
[Route("/api/message/update")]
public class BotController
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly ICommands _commands;

    public BotController(ITelegramBotClient telegramBotClient, ICommands serviceCommands)
    {
        _telegramBotClient = telegramBotClient;
        _commands = serviceCommands;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Update? update)
    {
        if (update?.Message?.Text == null) return new OkResult();

        var request = update.Message.Parse();
        if (request == null)
        {
            await _telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, "command start with '/' ");
            return new OkResult();
        }

        if (!_commands.Contains(request.CommandName))
        {
            await _commands.Get(BaseCommand.ExceptionUnknownCommand).Execute(request, _telegramBotClient);
            return new OkResult();
        }

        var command = _commands.Get(request.CommandName);
        await command.Execute(request, _telegramBotClient);

        return new OkResult();
    }
}