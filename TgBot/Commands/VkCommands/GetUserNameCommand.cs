using System.ComponentModel;
using MyBotTg.Bot;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot.controller.BotController.Services;
using VkNet.Enums.Filters;
using VkNet.Model;
using Vostok.Logging.Abstractions;
using Message = Telegram.Bot.Types.Message;


namespace TgBot.Commands;

public class GetFeed : VkApiConnect, ICommand
{
    public string Name { get; } = "/myself";
    public string desc { get; } = "Post From vk";
    private readonly IAccountVkRepository _accountVkRepository;
    private readonly ILog _log;

    public GetFeed(IAccountVkRepository accountVkRepository, ILog log)
    {
        _accountVkRepository = accountVkRepository;
        this._log = log;
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
        var news = group.Where(x => x.Date?.DayOfYear == DateTime.Today.DayOfYear)
            .ToArray();
        if (news.Length == 0)
        {
            Console.WriteLine("Новостей нету");
            return;
        }


        foreach (var newsItem in news)
        {
            if (newsItem.Type == NewsTypes.Post)
                _log.Info($"{newsItem}");
            if (!string.IsNullOrEmpty(newsItem.Text))
                await bot.SendTextMessageAsync(chatId, newsItem.Text);
            if (newsItem.Attachments == null) continue;
            
            var photos = newsItem.Attachments.FirstOrDefault(x => x.Instance is Photo);
            if (photos == null) continue;
            if (photos.Instance == null) continue;
            _log.Info($"{photos}");
            var p = (photos.Instance as Photo)?.PhotoSrc.ToString();
            _log.Info($"{p}");
            if (newsItem.Photos != null)
            {
                var photoInTg = InputFile.FromString(p);
                await bot.SendPhotoAsync(chatId, photoInTg);
            }
           
        }
    }
}