using TgAvBot;
using TgAvBot.Data;
using TgAvBot.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<TgBot>();
        services.AddSingleton<IDataRepository, DataRepository>();
        services.AddSingleton<IAvParserService, AvParserService>();
    })
    .Build();

host.Run();
