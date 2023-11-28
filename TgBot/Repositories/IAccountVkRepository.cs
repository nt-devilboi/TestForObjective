using Telegram.Bot.Types;
using TgBot.controller.model;

namespace MyBotTg.Bot;

public interface IAccountVkRepository
{
    public Task Add(AccessToken tokenResponse, long chatId);
    public Task<string?> GetAccessTokenByChat(long chatId);
    public Task Remove(int id);
}