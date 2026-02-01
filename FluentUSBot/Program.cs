using FluentUSBot.Services;
using System.Text.Json;
using Telegram.Bot;

namespace FluentUSBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("appsettings.json"));
            var botToken = config["TelegramBotToken"];

            var botClient = new TelegramBotClient(botToken);
            using var cts = new CancellationTokenSource();

            var freeDictionaryService = new FreeDictionaryService();

            var handler = new BotHandler(botClient, freeDictionaryService);
            botClient.StartReceiving(
                (client, update, token) => handler.HandleUpdateAsync(update, token),
                (client, exception, token) => handler.HandleErrorAsync(exception, token),
                cancellationToken: cts.Token
            );  

            Console.WriteLine("FluentUS Bot is started. Click Enter to exit");
            Console.ReadLine();
            cts.Cancel();

        }
    }


}
