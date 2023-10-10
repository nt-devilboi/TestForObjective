using System.Net;
using Newtonsoft.Json;
using TgBot;
using TgBot.controller.model;
using TgBot.ExtentionHttpContext;
using Vostok.Logging.Abstractions;

namespace MyBotTg.Bot;

public class AppAuth : IAppAuth
{
    private readonly AppSettings _settings;
    private readonly string _urlAuth = AuthRequest.AuthUrl;
    private readonly ILog _log;
    
    public AppAuth(AppSettings settings, ILog log)
    {
        _log = log;
        _settings = settings;
    }
    
    public async Task<AccessTokenResponse> GetAccessToken(string code)
    {
        _log.Info($"Code on server: {code}");
        var requestAuth = CreateRequestOnAuth(code);
        
        using var httpClient = new HttpClient();
        var responseMessage = await httpClient.GetAsync(requestAuth);
        if (responseMessage.StatusCode != HttpStatusCode.OK)
        {
            _log.Error("Failed to Get access token");
            return null;
        }
        
        var tokenResponse = await responseMessage.Content.JsonDeserialase<AccessTokenResponse>();
        httpClient.Dispose();
        _log.Info($"info token: {tokenResponse}");
        return tokenResponse;
    }

    private string CreateRequestOnAuth(string code)
    {
        return $"https://oauth.vk.com/access_token?" +
               $"client_id={_settings.clientId}&" +
               $"client_secret={_settings.ClientSecret}&" +
               $"redirect_uri={_urlAuth}&" +
               $"code={code}";
    }
}