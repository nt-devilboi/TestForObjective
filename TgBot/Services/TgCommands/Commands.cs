using System.ComponentModel;
using System.Reflection;
using TgBot.Commands;
using ICommand = TgBot.controller.BotController.Services.ICommand;

namespace TgBot.controller.model;

public class Commands : ICommands
{
    private readonly Dictionary<string, ICommand> _commands;
    private readonly List<InfoCommand> _infoCommands;

    public Commands()
    {
        _infoCommands = new List<InfoCommand>();
        _commands = new Dictionary<string, ICommand>();
        
        Add(new HelpCommand(_infoCommands));
    }
    public void Add(ICommand command)
    {
        if (_commands.ContainsKey(command.Name))
        {
            throw new InvalidOperationException("This command existed yet");
        }

        var descriptionAttribute = command.GetType().GetCustomAttribute<DescriptionAttribute>();

        if (descriptionAttribute == null)
        {
            throw new Exception("Не добавил описание");
        }
        var info = new InfoCommand() { Info = $"{command.Name} - {descriptionAttribute.Description}" };
        _infoCommands.Add(info);
        _commands.Add(command.Name, command);
    }

    public bool Contains(string commandName)
    {
        return _commands.ContainsKey(commandName);
    }

    public ICommand Get(string commandName)
    {
        if (!_commands.ContainsKey(commandName))
        {
            throw new ArgumentException("this Command Not Existed");
        }

        return _commands[commandName];
    }
}