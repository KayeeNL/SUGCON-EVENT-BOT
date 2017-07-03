using System;
using Newtonsoft.Json;

namespace SUGCON.Event.Bot.Models
{
    [Serializable]
    public class Speaker
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photo")]
        public string ImageUrl { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }
    }
}