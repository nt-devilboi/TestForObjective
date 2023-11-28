using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualBasic;
using MyBotTg.Bot;
using Telegram.Bot;
using TgBot.controller.BotController.Services;
using TgBot.controller.model;

namespace TgBot;

public static class ExtensionBotTg
{
    public static void AddTelegramBot(this IServiceCollection serviceCollection,
        string host, string token)
    {
        var client = new TelegramBotClient(token); // todo: Put  up in  EviromentVar
        var webhook = $"{host}/api/message/update";
        client.SetWebhookAsync(webhook).Wait();
        serviceCollection.AddSingleton<ITelegramBotClient>(client);
    }

    public static void AddTelegramCommands(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ICommands, controller.model.Commands>();
    }

    public static void UseCommands(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        
        var commands = services.GetService<ICommands>();
        if (commands == null)
        {
            throw new ApplicationException("NOT HAVE ADDED serviceCommands");
        }

        var assembly = Assembly.GetExecutingAssembly();
        var typesCommands = assembly
            .GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
            .ToArray();

        foreach (var typeCommand in typesCommands)
        {
            var constructor = typeCommand.GetConstructors()[0];
            var parameters = constructor.GetParameters();

            var servicesCommand = new List<object>();
            foreach (var parameter in parameters)
            {
                servicesCommand.Add(services.GetService(parameter.ParameterType)); //todo: здесь немного кринж с InfoCommand поэтому без requers
            }

            var command = assembly.CreateInstanceCommand(servicesCommand, typeCommand.FullName);
            if (command == null) throw new ArgumentException();
            
            commands.Add(command);
        }
        //todo: куча кода, где сначала мы будет доставать классы наследуемые Icommand, потом ходить по каждому смотреть какие есть свойства, потом доставать их и искать в сервисах, потом перекидывать в этот класс) и его добавлять в commands
    }

    public static ICommand? CreateInstanceCommand(this Assembly assembly, List<object> servicesForCommand, string fullName)
    {
        return assembly.CreateInstance(fullName,
            false,
            BindingFlags.Public | BindingFlags.Instance,
            null,
            servicesForCommand.ToArray(), CultureInfo.CurrentCulture,
            null) as ICommand;
    }
}