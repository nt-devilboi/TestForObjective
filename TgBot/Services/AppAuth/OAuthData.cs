namespace UlearnTodoTimer.Infrasturcture.Services.AppAuth;

public record OAuthData
{
    public string RedirectUrl;
    public string ResponseType;
    public string Version;
    public string Scope;
    public string ClientSecret;
    public string ClientId;
    public string ServiceOAuth;
    public string UriAuthorization;
    public string UriGetAccessToken;
    public string Display;
}