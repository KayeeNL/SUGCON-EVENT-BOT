using System.Collections.Generic;
using Newtonsoft.Json;

namespace SUGCON.Event.Bot.Models
{
    public class JokeValue
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("joke")]
        public string Joke { get; set; }

        [JsonProperty("categories")]
        public List<string> Categories { get; set; }
    }
}