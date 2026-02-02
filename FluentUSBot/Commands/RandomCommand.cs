using FluentUSBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FluentUSBot.Commands
{
    internal class RandomCommand : IBotCommand
    {
        public string Name => "/random";
        private readonly TelegramBotClient _botClient;
        private readonly FreeDictionaryService _freeDictionaryService;

        public RandomCommand(TelegramBotClient botClient, FreeDictionaryService freeDictionaryService)
        {
            _botClient = botClient;
            _freeDictionaryService = freeDictionaryService;
        }

        // Execute the /random command
        public async Task ExecuteAsync(Message message)
        {
            // TODO: Implement random word fetching logic
        }
    }
}
