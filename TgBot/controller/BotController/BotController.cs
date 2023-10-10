using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBot.controller.model;
using Vostok.Logging.Abstractions;

namespace TgBot.controller.BotController;

[ApiController]
[Route("/api/message/update")]
public class BotController : ControllerBase
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly IServiceCommands _commands;
    private readonly ILog _log;
    public BotController(ILog log, ITelegramBotClient telegramBotClient, IServiceCommands serviceCommands)
    {
        _telegramBotClient = telegramBotClient;
        _commands = serviceCommands;
        _log = log;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _log.Info("hello");
        return new OkResult();
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Update update)
    {
        if (update == null) return new OkResult();
        
        var message = update.Message;
        if (message.Text == null)
            return new OkResult();

        var request = message.Parse();

        if (request == null)
        {
            _log.Info("Not Command");
            return new OkResult();
        }
        
        if (!_commands.Have(request.CommandName))
        {
            _log.Info($"Unknown Command: {request.CommandName}");
            return new OkResult();
        }
            
        var command = _commands.Get(request.CommandName);
        
        _log.Info($"command run {request.CommandName}");
        await command.Execute(request, _telegramBotClient);

        return new OkResult();
    }
}