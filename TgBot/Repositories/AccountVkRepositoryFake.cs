using System.Collections.Concurrent;
using MyBotTg.Bot;
using Telegram.Bot.Types;
using TgBot.controller.model;

namespace TgBot;

public class AccountVkRepositoryFake : IAccountVkRepository
{
    private readonly ConcurrentDictionary<string, string> _concurrentDictionary = new ConcurrentDictionary<string, string>();
    public async Task Add(AccessToken tokenResponse, long chatId)
    {
        _concurrentDictionary.TryAdd(chatId.ToString(), tokenResponse.Token);
    }

    public async Task<string?> GetAccessTokenByChat(long chatId)
    {
        if (_concurrentDictionary.TryGetValue(chatId.ToString(), out var token))
        {
            return token;
        }

        return null;
    }

    public async Task Remove(int id)
    {
        throw new NotImplementedException();
    }
}