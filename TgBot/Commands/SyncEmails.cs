using System.Net;
using System.Net.Mail;
using MediatR;
using MyBotTg.Bot;
using Telegram.Bot;
using TgBot.Application;
using TgBot.Repositories;
using Vostok.Logging.Abstractions;
using ICommand = TgBot.controller.BotController.Services.ICommand;

namespace TgBot.Commands;

public class SyncEmails : ICommand
{
    private readonly IMailRepository _mails;
    private readonly IMediator _mediator;
    private readonly IApartmentRepo _aparts;
    private readonly ILog _log;

    public SyncEmails(IMailRepository mails, ILog log, IMediator mediator, IApartmentRepo aparts)
    {
        _mails = mails;
        _log = log;
        _mediator = mediator;
        _aparts = aparts;
    }

    public string Name { get; } = "/syncEmail";
    public string desc { get; } = "if you want to get info about apart to email use this command";

    public async Task Execute(IRequest? request, ITelegramBotClient bot)
    {
        var chatId = request.Message.Chat.Id;
        var mails = await _mails.GetAllMailByChatId(chatId);

        MailAddress from = new MailAddress("clashofnaks@gmail.com", "nikita");

        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("clashofnaks@gmail.com", "chsk rupp emre epcn"),
            EnableSsl = true
        };
        var apart = await _aparts.GetAll(chatId);
        var result= await _mediator.Send(new GetInfoApart(apart));
        if (result.IsEmpty)
        {
            await bot.SendTextMessageAsync(chatId, "you didn't add apart");
            return;
        }

        var infoAparts = result.ResponseDataAparts;
        foreach (var mail in mails)
        {
            _log.Info($"send to {mail.Address}");
            var to = new MailAddress(mail.Address);
            var m = new MailMessage(from, to);
            smtp.SendCompleted += (sender, args) => _log.Info($"sync with {to.Address}");
            
            m.Subject = "Актуальные цены на квартиры";
            m.Body = string.Join(Environment.NewLine, infoAparts.Select(x => x.ToString()));


            smtp.Send(m);
            _log.Info($"sent to {mail.Address}");
        }

        await bot.SendTextMessageAsync(chatId, "message send to all Email");
        _log.Info($"sent to all mail");
    }
}