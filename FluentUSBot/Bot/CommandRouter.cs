using FluentUSBot.Commands;
using Telegram.Bot.Types;

namespace FluentUSBot.Bot
{
    internal class CommandRouter
    {
        private readonly Dictionary<string, IBotCommand> _commands;

        public CommandRouter(IEnumerable<IBotCommand> router)
        {
            _commands = router.ToDictionary(c => c.Name);
        }

        internal async Task RouteAsync(Message message)
        {
            var command = (message.Text ?? string.Empty)
                .Trim()
                .ToLower()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault() ?? "/word";

            var botCommand = _commands.TryGetValue(command, out var cmd)
                ? cmd
                : _commands["/word"];

            await botCommand.ExecuteAsync(message);
        }
    }
}
