using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;

namespace TelegramBotExperiments
{

    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("5353144607:AAGMQVbmZB6gyUDEd0O_BDk1Gl9gktfQ_bM");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                string txt = message.Text.ToLower();
                if (int.TryParse(txt, out int number))
                {
                    string BinaryCode = Convert.ToString(number, 2);
                    await botClient.SendTextMessageAsync(message.Chat, BinaryCode);
                    await botClient.SendTextMessageAsync(message.Chat, "(^_~)");
                    return;
                }
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать. Введите число и я переведу его в двоичную систему счисления (◕‿◕)");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Я вас не понимаю... Введите число.");
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
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
            Console.ReadLine();
        }
    }
}