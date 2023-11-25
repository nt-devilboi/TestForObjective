using System.ComponentModel;
using MyBotTg.Bot;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.controller.BotController.Services;
using TgBot.controller.model;
using VkNet;
using VkNet.Abstractions;

namespace TgBot.Commands;
public class AddVkToken :VkApiConnect, ICommand
{
    public string Name { get; } = "/token";
    public string desc { get; } = "вставить токен, после /token пробела вести токен";
    public string Description { get; } = "Auth By Vk";
    private readonly IAccountVkRepository _accountVkRepository;
    public AddVkToken(IAccountVkRepository accountVkRepository)
    {
        _accountVkRepository = accountVkRepository;
    }


    public async Task Execute(IRequest request, ITelegramBotClient bot)
    {
        var accessToken = new AccessToken() { Token = request.ExtraData };
        await _accountVkRepository.Add(accessToken, request.Message.Chat); // todo: проверка на то, что токен вообще загружен
        await bot.SendTextMessageAsync(request.Message.Chat.Id,"Токен загружен");
    }
}