using FluentUSBot.Models;
using System.Text.Json;

namespace FluentUSBot.Services
{
    internal class FreeDictionaryService
    {
        private readonly HttpClient _httpClient = new();

        public async Task<(string Word, string Definition, string Example)> GetWordDataAsync(string word)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"https://api.dictionaryapi.dev/api/v2/entries/en/{word}");
                var wordData = JsonSerializer.Deserialize<List<WordDataEntry>>(response);

                if (wordData != null && wordData.Count > 0)
                {
                    var firstEntry = wordData[0];
                    var firstMeaning = firstEntry.Meanings?.FirstOrDefault();
                    var firstDefinition = firstMeaning?.Definitions?.FirstOrDefault();

                    if (firstDefinition != null)
                        return (firstEntry.Word ?? word, firstDefinition.DefinitionText ?? "No definition available", firstDefinition.Example ?? "No example available");
                }
                
                return (word, "No data found", "No example available");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching word data: {ex.Message}");
                return (word, $"Error: {ex.Message}", "No example available");
            }
        }

        public async Task<List<string>> GetWordListAsync()
        {
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "Data", "words.txt");
                if (!File.Exists(path))
                {
                    Console.WriteLine($"Word list file not found at: {path}");
                    return new List<string>();
                }

                var content = await File.ReadAllLinesAsync(path);
                return content
                    .Select(w => w.Trim())
                    .Where(w => !string.IsNullOrWhiteSpace(w) && !w.StartsWith("#"))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading word list: {ex.Message}");
                return new List<string>();
            }
        }
    }
}
