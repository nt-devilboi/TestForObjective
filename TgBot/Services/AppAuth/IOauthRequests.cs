namespace UlearnTodoTimer.Services.AppAuth;

public interface IOauthRequests
{
    public string CreateAuthRequest(string state);
    public string CreateGetAccessTokenRequest(string code);
}