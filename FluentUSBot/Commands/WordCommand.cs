using FluentUSBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FluentUSBot.Commands
{
    internal class WordCommand : IBotCommand
    {
        public string Name => "/word";
        private readonly TelegramBotClient _botClient;
        private readonly FreeDictionaryService _freeDictionaryService;

        public WordCommand(TelegramBotClient botClient, FreeDictionaryService freeDictionaryService)
        {
            _botClient = botClient;
            _freeDictionaryService = freeDictionaryService;
        }

        // Execute the /word command
        public async Task ExecuteAsync(Message message)
        {
            var chatId = message.Chat.Id;
            var text = message.Text!.Trim();

            if (text.Length < 6)
            {
                await _botClient.SendMessage(chatId, "Please provide a word after the /word command. Example: /word example");
                return;
            }

            var word = text.Substring(6).Trim();
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
