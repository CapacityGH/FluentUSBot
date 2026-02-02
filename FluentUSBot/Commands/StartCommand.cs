using FluentUSBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FluentUSBot.Commands
{
    internal class StartCommand : IBotCommand
    {
        public string Name => "/start";
        private readonly TelegramBotClient _botClient;
        private readonly FreeDictionaryService _freeDictionaryService;

        public StartCommand(TelegramBotClient botClient, FreeDictionaryService freeDictionaryService)
        {
            _botClient = botClient;
            _freeDictionaryService = freeDictionaryService;
        }

        // Execute the /start command
        public async Task ExecuteAsync(Message message)
        {
            // TODO: Implement start command logic
        }
    }
}