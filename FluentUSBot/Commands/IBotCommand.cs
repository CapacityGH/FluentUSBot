using Telegram.Bot.Types;

namespace FluentUSBot.Commands
{
    internal interface IBotCommand
    {
        string Name { get; }
        Task ExecuteAsync(Message message);
    }
}
