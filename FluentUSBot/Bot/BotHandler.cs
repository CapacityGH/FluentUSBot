using FluentUSBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FluentUSBot.Bot
{
    internal class BotHandler
    {
        private readonly CommandRouter _commandRouter;
        
        public BotHandler(CommandRouter commandRouter)
        {
            _commandRouter = commandRouter;
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
            
            await _commandRouter.RouteAsync(update.Message);
        }
    }
}