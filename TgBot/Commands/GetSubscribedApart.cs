using System.Diagnostics;
using System.Text.Json;
using MediatR;
using MyBotTg.Bot;
using Newtonsoft.Json.Linq;
using Telegram.Bot;
using TgBot.Application;
using TgBot.controller.BotController.Services;
using TgBot.Domain.Entity;
using TgBot.Repositories;
using TgBot.Repositories;
using Vostok.Logging.Abstractions;

namespace TgBot.Commands;

public class GetApart : ICommand
{
    private readonly IMailRepository _mails;
    private readonly IApartmentRepo _apartments;
    private readonly ILog _log;
    private readonly IMediator _mediator;
    public GetApart(IMailRepository mails, IApartmentRepo apartments, ILog log, IMediator mediator)
    {
        _mails = mails;
        _apartments = apartments;
        _log = log;
        _mediator = mediator;
    }

    public string Name { get; } = "/getApart";
    public string desc { get; } = "Get all Subscribed apartment";
    
    public async Task Execute(IRequest? request, ITelegramBotClient bot)
    {
        var aparts = await _apartments.GetAll(request.Message.Chat.Id);

        var result = await _mediator.Send(new GetInfoApart(aparts));
        if (result.IsEmpty)
        {
            await bot.SendTextMessageAsync(request.Message.Chat.Id, "you must add apart before use this command");
            return;
        }

        var infoPriceApart = result.ResponseDataAparts; 
        await bot.SendTextMessageAsync(request.Message.Chat.Id, string.Join(Environment.NewLine,infoPriceApart.Select(x => x.ToString())));
    }

   

     
    
}