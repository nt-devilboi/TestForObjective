using System.Net;   
using TgBot.controller.model;
using TgBot.ExtentionHttpContext;
using UlearnTodoTimer.Infrasturcture.Services.AppAuth;

namespace MyBotTg.Bot;

public class AppAuth: IAppAuth
{
    private readonly IProvideOAuth _settings;
    
    public AppAuth(IProvideOAuth settings)
    {
        _settings = settings;
    }
    
    public async Task<AccessTokenResponse?> GetAccessToken(string code)
    {
        var vkOAuth = _settings.GetOAuth("vk");
        
        var requestAuth = vkOAuth.CreateGetAccessTokenRequest(code);
        var responseAuth = await new HttpClient().GetAsync(requestAuth);
        
        if (responseAuth.StatusCode != HttpStatusCode.OK) return null;
        
        return await responseAuth.Content.JsonDeserialse<AccessTokenResponse>();;
    }
}