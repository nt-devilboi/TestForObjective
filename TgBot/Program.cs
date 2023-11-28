using MyBotTg.Bot;
using TgBot;
using TgBot.controller.BotController.Services;
using TgBot.controller.model;
using VkNet;
using VkNet.Abstractions;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;
using Vostok.Logging.Microsoft;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var log = new ConsoleLog(new ConsoleLogSettings()
{
    ColorsEnabled = true
});
builder.Logging.ClearProviders();
builder.Logging.AddVostok(log);
builder.Services.AddSingleton<ILog>(log);

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAppAuth, AppAuth>();
builder.Services.AddSingleton<AppSettings>(_ => AppSettings.FromEnv());
builder.Services.AddSingleton<IAccountVkRepository, AccountVkRepositoryFake>();
builder.Services.AddTelegramCommands();
builder.Services.AddTransient<IVkApi, VkApi>(_ =>  new VkApi());

builder.Services.AddTelegramBot("https://d1e6-5-165-24-116.ngrok.io", "6184368668:AAHhdVpR7WvBzM6qFaR1EnWpLBIw4v72tq0");




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