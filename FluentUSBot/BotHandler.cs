using FluentUSBot.Services;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FluentUSBot
{
    internal class BotHandler
    {
        private readonly TelegramBotClient _botClient;
        private readonly FreeDictionaryService _freeDictionaryService;

        public BotHandler(TelegramBotClient botClient, FreeDictionaryService freeDictionaryService)
        {
            _botClient = botClient;
            _freeDictionaryService = freeDictionaryService;
        }

        internal async Task HandleErrorAsync(Exception exception, CancellationToken token)
        {
            Console.WriteLine($"Error occurred: {exception.Message}");
            await Task.CompletedTask;
        }

        internal async Task HandleUpdateAsync(Update update, CancellationToken token)
        {
            if (update.Type != UpdateType.Message || update.Message?.Text == null)
                return;

            // update is 
            var message = update.Message.Text.ToLower();
            var chatId = update.Message.Chat.Id;

            if (message.StartsWith("/start"))
            {
                await _botClient.SendMessage(chatId, "Hello! Welcome to the FluentUS Bot. Send me a word, and I'll provide you with its definition and an example sentence.");
            }
            else if (message.StartsWith("/word"))
            {
                if (message.Length < 6)
                {
                    await _botClient.SendMessage(chatId, "Please provide a word after the /word command. Example: /word example");
                    return;
                }

                var word = message.Substring(6).Trim();
                var data = await _freeDictionaryService.GetWordDataAsync(word);
                
                if (!string.IsNullOrEmpty(data.Word))
                {
                    var response = $"Word for today: *{data.Word}*\nDefinition: {data.Definition}\nExample: {data.Example}";
                    await _botClient.SendMessage(chatId, response, ParseMode.Markdown);
                }
                else
                {
                    await _botClient.SendMessage(chatId, $"Sorry, I couldn't find the word '{word}'. Please try another word.");
                }

            }
        }
    }
}