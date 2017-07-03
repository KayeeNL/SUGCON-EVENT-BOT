using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SUGCON.Event.Bot.Models
{
    [Serializable]
    public class Session
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("speakers")]
        public List<Speaker> Speakers { get; set; }

        [JsonProperty("from")]
        public DateTime From { get; set; }

        [JsonProperty("until")]
        public DateTime Until { get; set; }

        [JsonProperty("room")]
        public int Room { get; set; }

        [JsonProperty("speakersimage")]
        public string SpeakersImage { get; set; }
    }
}