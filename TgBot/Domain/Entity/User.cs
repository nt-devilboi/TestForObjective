using System.ComponentModel.DataAnnotations.Schema;

namespace TgBot.Domain.Entity;

[Table("Mail", Schema = "ObjectiveTest")]
public class Mail
{
    public string Address { get; set; }
    public Guid Id { get; set; }
    public long ChatId { get; set; }


    public static Mail From(string email, long chatId)
        => new()
        {
            ChatId = chatId,
            Address = email,
            Id = Guid.NewGuid()
        };
}