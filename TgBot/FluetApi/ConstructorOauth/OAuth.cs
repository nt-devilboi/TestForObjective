using UlearnTodoTimer.Infrasturcture.Services.AppAuth;
using UlearnTodoTimer.Services.AppAuth;

namespace UlearnTodoTimer.FluetApi.ConstructorOauth;

public class OAuths: IAddOAuth, IProvideOAuth
{
    private readonly Dictionary<string, IOauthRequests> _oauthRequestsMap = new Dictionary<string, IOauthRequests>();

    public OAuths AddOAuth(string name, Action<ConstructorOAuth> ConfigureOAuth)
    {
        if (name == string.Empty)
        {
            throw new ArgumentException("name is empty");
        }
        
        if (ConfigureOAuth == null)
        {
            throw new ArgumentNullException(nameof(ConfigureOAuth));
        }
        
        var ctorOAuth = new ConstructorOAuth();
        ConfigureOAuth(ctorOAuth);
        _oauthRequestsMap.Add(name ,ctorOAuth.Build());
        
        return this;
    }
    
    public IOauthRequests GetOAuth(string name)
    {
        
        if (!_oauthRequestsMap.TryGetValue(name, out var value))
        {
            throw new ArgumentException();
        }
        
        Console.WriteLine(value);
        return value;
    }

    public static IAddOAuth CreateBuilder()
    {
        return new OAuths();
    }
}