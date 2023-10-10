using System.ComponentModel;
using MyBotTg.Bot;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.controller.BotController.Services;
using VkNet.Model;
using Message = Telegram.Bot.Types.Message;


namespace TgBot.Commands;

[Description("Post From vk")]
public class GetUserNameCommand : VkApiConnect, ICommand
{
    public string Name { get; } = "/myself";
    private readonly IAccountVkRepository _accountVkRepository;

    public GetUserNameCommand(IAccountVkRepository accountVkRepository)
    {
        _accountVkRepository = accountVkRepository;
    }


    public async Task Execute(IRequest request, ITelegramBotClient bot)
    {
        var chatId = request.Message.Chat.Id;
        var token = await _accountVkRepository.GetAccessTokenByChat(chatId);
        if (token == null)
        {
            await bot.SendTextMessageAsync(chatId, "упс сначала войти в вк");
            return;
        }

        Console.WriteLine(token);
        AuthByToken(token);
        var feed = _vkApi.NewsFeed.Get(new NewsFeedGetParams() { });
        var group = feed.Items;
        var news = group.FirstOrDefault();
        if (news == null)
        {
            Console.WriteLine("Новостей нету");
            return;
        }/*
        var photo = news.Photos.First();
        var photoInTg = InputFile.FromString(photo.Photo1280.ToString());*/

        var text = news.Text;
        await bot.SendTextMessageAsync(chatId, text);
    }
}