namespace TgBot.Commands;

public class OutSideApp<T> where T: new() {
    protected T App = new();
}