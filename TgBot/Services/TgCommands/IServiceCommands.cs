using TgBot.controller.BotController.Services;

namespace TgBot.controller.model;

public interface IServiceCommands
{
    public bool Have(string commandName);
    public ICommand Get(string commandName);
}