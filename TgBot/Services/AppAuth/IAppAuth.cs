using TgBot.controller.model;

namespace UlearnTodoTimer.Repositories;

public interface IAppAuth
{
    public Task<AccessTokenResponse?> GetAccessToken(string getAccessTokenRequest);
}