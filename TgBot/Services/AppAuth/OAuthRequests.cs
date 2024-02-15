using UlearnTodoTimer.Services.AppAuth;

namespace UlearnTodoTimer.Infrasturcture.Services.AppAuth;

public class OAuthRequests : IOauthRequests
{
    public readonly OAuthData OAuthData;
    public string Scope => OAuthData.Scope;

    public OAuthRequests(OAuthData oAuthData)
    {
        OAuthData = oAuthData;
    }

    public string CreateAuthRequest(string state = "")
    {
        var req =  $"{OAuthData.ServiceOAuth}/{OAuthData.UriAuthorization}?" +
            $"client_id={OAuthData.ClientId}&" +
            SetState(state) +
            GetQueryDisplay() + 
            $"scope={OAuthData.Scope}&" +
            $"redirect_uri={OAuthData.RedirectUrl}&" +
            $"response_type={OAuthData.ResponseType}&" +
            GetQueryV(); 
        
        Console.WriteLine(req);

        return req;
    }

    private string GetQueryDisplay()
        => OAuthData.Display != string.Empty ? $"display={OAuthData.Display}&" : string.Empty;
    
    private string SetState(string state)
        => state != string.Empty ? $"state={state}&" : string.Empty;

    private string GetQueryV()
        => OAuthData.Version != string.Empty ? $"v={OAuthData.Version}" : string.Empty;
    public string CreateGetAccessTokenRequest(string code)
    {
        return $"{OAuthData.ServiceOAuth}/{OAuthData.UriGetAccessToken}?" +
               $"client_id={OAuthData.ClientId}&" +
               $"client_secret={OAuthData.ClientSecret}&" +
               $"redirect_uri={OAuthData.RedirectUrl}&" +
               $"code={code}";
    }
}