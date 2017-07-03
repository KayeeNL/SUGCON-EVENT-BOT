using System;
using Newtonsoft.Json;

namespace SUGCON.Event.Bot.Models
{
    [Serializable]
    public class Room
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}