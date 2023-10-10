using Microsoft.AspNetCore.Mvc;
using MyBotTg.Bot;
using Telegram.Bot;
using Telegram.Bot.Types;
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
    
    public AuthController(ITelegramBotClient client, IAppAuth appAuth, ILog log, IAccountVkRepository accountVkRepository)
    {
        _client = client;
        _appAuth = appAuth;
        _log = log;
        _accountVkRepository = accountVkRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Auth(string code)
    {
        var accessTokenResponse = await _appAuth.GetAccessToken(code);
        _log.Info($"token {accessTokenResponse}");
        if (accessTokenResponse == null) return new StatusCodeResult(404);
        
        
        return Ok($"введи токен в боте {accessTokenResponse.AccessToken}");
    }

}