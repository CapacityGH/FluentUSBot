using FluentUSBot.Services;
using Telegram.Bot;

namespace FluentUSBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var botToken = Environment.GetEnvironmentVariable("TelegramBotToken");
            if (string.IsNullOrWhiteSpace(botToken))
            {
                Console.WriteLine("Error: TelegramBotToken is not set");
                return;
            }

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
            await Task.Delay(-1);
        }
    }
}
