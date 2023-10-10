using Microsoft.VisualBasic;
using TgBot.controller.BotController.Services;

namespace TgBot.controller.model;

public interface ICommands
{
    public void Add(ICommand command);
    public bool Contains(string commandName);
    public ICommand Get(string commandName);
}