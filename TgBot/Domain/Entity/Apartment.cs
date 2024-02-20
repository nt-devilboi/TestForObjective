using System.ComponentModel.DataAnnotations.Schema;

namespace TgBot.Domain.Entity;

[Table("Apartment", Schema = "ObjectiveTest")]
public class Apartment
{
    public Guid Id { get; set; }
    public long CharId { get; set; }
    public string UrlApart { get; set; }

    public static Apartment From(string url, long chatId)
        => new()
        {
            UrlApart = url,
            CharId = chatId,
            Id = Guid.NewGuid()
        };
}