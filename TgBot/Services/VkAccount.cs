using MyBotTg.Bot;
using VkNet;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace TgBot.Services;

public class VkAccount : IAccount<IVkApi>
{
    private readonly AuthWebSiteSettings _settings;

    public VkAccount(AuthWebSiteSettings settings)
    {
        _settings = settings;
    }

    public IVkApi Get(string token)
    {
        var account = new VkApi();
        account.Authorize(new ApiAuthParams()
        {
            AccessToken = token,
            Settings = Settings.FromJsonString(AuthWebSiteSettings.Scope)
        });

        return account;
    }
}