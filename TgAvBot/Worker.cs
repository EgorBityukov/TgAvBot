using TgAvBot.Services;

namespace TgAvBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly TgBot _tgBot;
        private readonly IAvParserService _avParserService;
        private readonly IUserService _userService;

        public Worker(ILogger<Worker> logger, TgBot tgBot, IAvParserService avParserService, IUserService userService)
        {
            _logger = logger;
            _tgBot = tgBot;
            _avParserService = avParserService;
            _userService = userService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _tgBot.StartBot();
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var users = _userService.GetUsers();

                foreach (var user in users)
                {
                    if (user.URL != null)
                    {
                        var carsForUser = _avParserService.ParseCarsForUser(user.URL);

                        foreach (var car in carsForUser)
                        {
                            await _tgBot.SendMessage(user.ChatId, car.Url);
                        }
                    }
                }
                
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}