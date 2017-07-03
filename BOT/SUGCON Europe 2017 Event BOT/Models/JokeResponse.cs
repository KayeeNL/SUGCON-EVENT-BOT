using Newtonsoft.Json;

namespace SUGCON.Event.Bot.Models
{
    public class JokeResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public JokeValue Value { get; set; }
    }
}