using Microsoft.AspNetCore.Mvc;
using MyBotTg.Bot;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.controller.model;
using TgBot.ExtentionHttpContext;
using Vostok.Logging.Abstractions;

namespace TgBot.controller;

[ApiController]
[Route("oauth/bot")]
public class AuthController : ControllerBase
{
    private readonly ILog _log;
    private readonly IAppAuth _appAuth;
    private readonly IAccountVkRepository _accountVkRepository;
    private readonly ITelegramBotClient _client;

    public AuthController(ITelegramBotClient client, 
        IAppAuth appAuth,
        ILog log,
        IAccountVkRepository accountVkRepository)
    {
        _client = client;
        _appAuth = appAuth;
        _log = log;
        _accountVkRepository = accountVkRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Auth([FromQuery] string code, [FromQuery(Name = "state")] string chatId)
    {
        _log.Info($"receive code {code} for chatId {chatId}");
        
        var accessTokenResponse = await _appAuth.GetAccessToken(code);
        _log.Info($"token {accessTokenResponse}");

        if (accessTokenResponse == null) return NotFound("token not receive");
        
        await _accountVkRepository.Add(accessTokenResponse, long.Parse(chatId));
        await _client.SendTextMessageAsync(chatId, "Authorization occured successful");

        return Ok();
    }
}