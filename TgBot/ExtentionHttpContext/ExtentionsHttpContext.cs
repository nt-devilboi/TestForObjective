using Newtonsoft.Json;

namespace TgBot.ExtentionHttpContext;

public static class ExtensionsttpContext
{
    public static async Task<T> JsonDeserialase<T>(this HttpContent httpContent)
    {
        var authJson = await httpContent.ReadAsStringAsync();
        var date = JsonConvert.DeserializeObject<T>(authJson);
        return date;
    }
}