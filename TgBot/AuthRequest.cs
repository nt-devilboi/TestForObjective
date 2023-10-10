namespace TgBot;

public class AuthRequest
{
    private static string host = "http://localhost:5128";
    public static string AuthUri = "OAuth/Bot";
    public static string AuthUrl = $"{host}/{AuthUri}";
}