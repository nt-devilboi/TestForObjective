using Telegram.Bot.Types;
using TgBot.controller.model;

namespace MyBotTg.Bot;

public interface IAccountVkRepository
{
    public Task Add(AccessToken tokenResponse, Chat chat);
    public Task<string?> GetAccessTokenByChat(long chatId);
    public Task Remove(int id);
}