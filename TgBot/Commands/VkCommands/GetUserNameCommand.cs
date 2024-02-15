using System.ComponentModel;
using MyBotTg.Bot;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.controller.BotController.Services;
using TgBot.Services;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model;
using Vostok.Logging.Abstractions;


namespace TgBot.Commands;

public class GetFeed : ICommand
{
    public string Name { get; } = "/friends";
    public string desc { get; } = "Post From vk";
    private readonly IAccountVkRepository _accountVkRepository;
    private readonly IAccount<IVkApi> _vkAccounts;
    private readonly ILog _log;

    public GetFeed(IAccountVkRepository accountVkRepository, ILog log, IAccount<IVkApi> vkAccounts)
    {
        _accountVkRepository = accountVkRepository;
        this._log = log;
        _vkAccounts = vkAccounts;
    }


    public async Task Execute(IRequest? request, ITelegramBotClient bot)
    {
        var chatId = request.Message.Chat.Id;
        var token = await _accountVkRepository.GetAccessTokenByChat(chatId);
        if (token == null)
        {
            await bot.SendTextMessageAsync(chatId, "упс сначала войти в вк");
            return;
        }

        Console.WriteLine(token);
        var account = _vkAccounts.Get(token);

        var friends = await account.Friends.GetAsync(new FriendsGetParams());

        _log.Info($"{friends.TotalCount}");
        await bot.SendTextMessageAsync(chatId, "adfasdf");
    }
}