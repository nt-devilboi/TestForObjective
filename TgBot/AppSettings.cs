namespace TgBot;

public record AppSettings(string clientId, string ClientSecret)
{
    private static readonly string ClientIdEnv = "CLIENT_ID";
    private static readonly string ClientSecretEnv = "CLIENT_SECRET_BOT"; // todo not BOT. CODE

    public static AppSettings FromEnv()
    {
        var clientSecret = GetEnvVariable(AppSettings.ClientSecretEnv);
        var clientId = GetEnvVariable(AppSettings.ClientIdEnv);
        return new AppSettings(clientId, clientSecret);
    }

    private static string GetEnvVariable(string name)
    {
        var value = Environment.GetEnvironmentVariable(name)
                    ?? Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.User);
        if (value == null)
            throw new InvalidOperationException($"env variable '{name}' not found");
        return value;
    }
}