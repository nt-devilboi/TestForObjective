using MyBotTg.Bot;
using TgBot;
using TgBot.Services;
using UlearnTodoTimer.FluetApi.ConstructorOauth;
using UlearnTodoTimer.Infrasturcture.Services.AppAuth;
using VkNet.Abstractions;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;
using Vostok.Logging.Microsoft;

var builder = WebApplication.CreateBuilder(args);
var oAuth = OAuths.CreateBuilder();
oAuth.AddOAuth("vk", o =>
{
    o.SetRedirectUrl("http://localhost:5128/OAuth/Bot")
        .SetClientId("51749903")
        .SetScope("friends")
        .SetHostServiceOAuth("https://oauth.vk.com")
        .SetClientSecret(AuthWebSiteSettings.FromEnv().ClientSecretVk)
        .SetUriAuth("authorize")
        .SetVersion("5.131")
        .SetResponseType("code")
        .SetUriGetAccessToken("access_token")
        .SetDisplay("page");
});


// Add services to the container.
var log = new ConsoleLog(new ConsoleLogSettings()
{
    ColorsEnabled = true
});
builder.Logging.ClearProviders();
builder.Logging.AddVostok(log);
builder.Services.AddSingleton<ILog>(log);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAppAuth, AppAuth>();
builder.Services.AddSingleton<AuthWebSiteSettings>(_ => AuthWebSiteSettings.FromEnv());
builder.Services.AddSingleton<IProvideOAuth>(_ => oAuth as IProvideOAuth ?? throw new InvalidOperationException());
builder.Services.AddSingleton<IAccountVkRepository, AccountVkRepositoryFake>();
builder.Services.AddTelegramCommands();
builder.Services.AddTransient<IAccount<IVkApi>,VkAccount>();

builder.Services.AddTelegramBot("https://797c-5-165-24-134.ngrok-free.app", "6184368668:AAHhdVpR7WvBzM6qFaR1EnWpLBIw4v72tq0");




var app = builder.Build();
app.UseCommands();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();
    /*app.UseWhen(x => x.Request.Path.StartsWithSegments("/api"), c =>
{
    c.UseMiddleware<IsTelegramChatMiddleWare>();
});*/
app.Run();