namespace MyBotTg.Bot;

public interface IAccount<out T>
{
    public T Get(string token);
}