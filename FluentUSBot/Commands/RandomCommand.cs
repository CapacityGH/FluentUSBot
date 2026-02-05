using FluentUSBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FluentUSBot.Commands
{
    internal class RandomCommand : IBotCommand
    {
        public string Name => "/random";
        private readonly TelegramBotClient _botClient;
        private readonly FreeDictionaryService _freeDictionaryService;
        private readonly Random _random = new Random();

        public RandomCommand(TelegramBotClient botClient, FreeDictionaryService freeDictionaryService)
        {
            _botClient = botClient;
            _freeDictionaryService = freeDictionaryService;
        }

        // Execute the /random command
        public async Task ExecuteAsync(Message message)
        {
            // TODO: Implement random word fetching logic
            var chatId = message.Chat.Id;

            var allWords = await _freeDictionaryService.GetWordListAsync();

            if (allWords.Count == 0 || allWords == null)
            {
                await _botClient.SendMessage(chatId, "Sorry, no words are available at the moment.");
                return;
            }

            var randomWord = allWords[_random.Next(allWords.Count)];

            var data = await _freeDictionaryService.GetWordDataAsync(randomWord);

            if (!string.IsNullOrEmpty(data.Word))
            {
                var response = $"Random Word: *{data.Word}*\nDefinition: {data.Definition}\nExample: {data.Example}";
                await _botClient.SendMessage(chatId, response, ParseMode.Markdown);
            }
            else
            {
                await _botClient.SendMessage(chatId, $"Sorry, I couldn't fetch a random word. Please try again.");

            }
        }
    }
}