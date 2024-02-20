using System.Reflection;
using MyBotTg.Bot;
using TgBot;
using TgBot.Infrastucture.DataBase;
using TgBot.Repositories;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;
using Vostok.Logging.Microsoft;

var builder = WebApplication.CreateBuilder(args);



var log = new ConsoleLog(new ConsoleLogSettings()
{
    ColorsEnabled = true
});
builder.Logging.ClearProviders();
builder.Logging.AddVostok(log);
builder.Services.AddSingleton<ILog>(log);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMailRepository, MailRepo>();
builder.Services.AddSingleton<IApartmentRepo, ApartRepo>();
builder.Services.AddMediatR(cnf =>
{
    cnf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cnf.Lifetime = ServiceLifetime.Singleton;

});

builder.Services.AddTransient<DbObjective>();
builder.Services.AddTelegramCommands();

builder.Services.AddTelegramBot("https://e9a7-5-165-24-46.ngrok-free.app", "6184368668:AAHhdVpR7WvBzM6qFaR1EnWpLBIw4v72tq0");




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