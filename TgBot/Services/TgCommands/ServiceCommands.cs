using MyBotTg.Bot;
using TgBot.Commands;
using TgBot.controller.BotController.Services;
using VkNet;
using VkNet.Abstractions;

namespace TgBot.controller.model;

public class ServiceCommands : IServiceCommands
{
    private readonly ICommands _commands;
    private readonly IAccountVkRepository _accountVkRepository;
    
    public ServiceCommands(IVkApi vkApi, IAccountVkRepository accountVkRepository)
    {
        _commands = new Commands();
        _accountVkRepository = accountVkRepository;
        Configuration();
    }

    private void Configuration()
    {
        _commands.Add(new SimpleCommand());
        _commands.Add(new AuthVkCommand());
        _commands.Add(new CommandAddTokenVk(_accountVkRepository));
        _commands.Add(new GetUserNameCommand(_accountVkRepository));
    }


    public bool Have(string commandName)
    {
        return _commands.Contains(commandName);
    }

    public ICommand Get(string commandName)
    {
        return _commands.Get(commandName);
    }
}