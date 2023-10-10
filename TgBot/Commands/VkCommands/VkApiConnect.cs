using VkNet;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace TgBot.Commands;

public abstract class VkApiConnect // todo: можно сделать факбикрой, чтоб подключать не только api вк а еще других соц сетей сайтов!
{
    protected readonly IVkApi _vkApi;

    public VkApiConnect()
    {
        _vkApi = new VkApi();
    }

    public void AuthByToken(string token)
    {
        
        _vkApi.Authorize(new ApiAuthParams() {AccessToken = token, Settings = Settings.FromJsonString("wall,friends") });
    }
}