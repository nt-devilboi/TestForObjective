using Newtonsoft.Json;

namespace TgBot.ExtentionHttpContext;

public static class ExtensionsttpContext
{
    public static async Task<T?> JsonDeserialse<T>(this HttpContent httpContent)
    {
        var dataJson = await httpContent.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(dataJson);
    }
}