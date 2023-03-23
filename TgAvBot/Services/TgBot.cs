using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TgAvBot.Services
{
    public class TgBot
    {
        private readonly ITelegramBotClient bot;
        private readonly IUserService _userService;

        public TgBot(IUserService userService)
        {
            _userService = userService;
            bot = new TelegramBotClient("6180960222:AAFF2MlaGa-jKvOfSTZtv7-G4MpOBJUEkcs");
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                var chatId = update.Message.Chat.Id;

                if (!_userService.FindUser(chatId))
                {
                    _userService.AddUser(chatId);
                }
                if (message != null && message.Text != null)
                {
                    if (message.Text.ToLower() == "/start")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать, перекуп!   (пид0р)");
                        return;
                    }
                    if (message.Text.ToLower().Contains("/addcar"))
                    {
                        var url = message.Text.ToLower().Replace("/addcar ", "");

                        if (!url.Contains("sort"))
                        {
                            url = url + "&sort=4";
                        }

                        _userService.AddUrlForUser(chatId, url);
                        await botClient.SendTextMessageAsync(message.Chat, "Подписка добавлена, зарабатывай свои миллионы");
                        return;
                    }
                    if (message.Text.ToLower() == "/deletecar")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Отмена подписки");
                        return;
                    }
                }

                await botClient.SendTextMessageAsync(message.Chat, "Не то пишешь перекупик)))00)");
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        public async Task StartBot()
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };

            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
        }

        public async Task SendMessage(ChatId chatId, string text)
        {
            Message message = await bot.SendTextMessageAsync(chatId, text);
        }
    }
}
