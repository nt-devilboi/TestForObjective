using Telegram.Bot;

namespace TgBot;

public static class ExtentionBotTg
{
    public static IServiceCollection AddTelegramBot(this IServiceCollection serviceCollection,
        string host, string token)
    {
        var client = new TelegramBotClient(token); // todo: Put  up in  EviromentVar
        var webhook = $"{host}/api/message/update";
        client.SetWebhookAsync(webhook).Wait();
        return serviceCollection.AddSingleton<ITelegramBotClient>(client);
    }
}