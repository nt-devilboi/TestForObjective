using UlearnTodoTimer.FluetApi.ConstructorOauth;
using UlearnTodoTimer.Services.AppAuth;

namespace UlearnTodoTimer.Infrasturcture.Services.AppAuth;

public interface IAddOAuth
{
    public OAuths AddOAuth(string name, Action<ConstructorOAuth> ConfigureOAuth);
}

public interface IProvideOAuth
{
    public IOauthRequests GetOAuth(string name);
}