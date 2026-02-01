using System.Text.Json.Serialization;

namespace FluentUSBot.Models
{
    internal class Definition
    {
        [JsonPropertyName("definition")]
        public string? DefinitionText { get; set; }

        [JsonPropertyName("example")]
        public string? Example { get; set; }
    }
}
