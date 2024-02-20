namespace TgBot.Dto;

public class ResponseDataApart
{
    public string UrlApart { get; set; }
    public long Price { get; set; }

    public ResponseDataApart(string urlApart, long price)
    {
        UrlApart = urlApart;
        Price = price;
    }
    
    public override string ToString()
    {
        return $"apartment: {UrlApart} have price {Price}";
    }
}