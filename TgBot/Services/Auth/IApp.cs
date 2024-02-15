using TgBot.controller.model;

namespace MyBotTg.Bot;

public interface IAppAuth
{
    Task<AccessTokenResponse?> GetAccessToken(string code);
    
}