using System.Text.Json.Serialization;

namespace FluentUSBot.Models
{
    internal class WordDataEntry
    {
        [JsonPropertyName("word")]
        public string? Word { get; set; }

        [JsonPropertyName("meanings")]
        public List<Meaning>? Meanings { get; set; }
    }
}
