using TgBot.Commands;
using TgBot.LibTgBot.BaseCommand;
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
        Add(new UnknownCommand());
        Add(new ThisNotCommand());
    }

    public void Add(ICommand command)
    {
        if (_commands.ContainsKey(command.Name))
        {
            return; //todo: менять либо убирать, либо менять логику HelpCommand
        }

        if (command.Name[0] == '/')
        {
            var info = new InfoCommand() { Info = $"{command.Name} - {command.desc}" };
            _infoCommands.Add(info);
        }

        
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
            throw new ArgumentException("this Command not Existed");
        }

        return _commands[commandName];
    }
}