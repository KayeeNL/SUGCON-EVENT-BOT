using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SUGCON.Event.Bot.Models
{
    [Serializable]
    public class SugconResponse
    {
        [JsonProperty("speakers")]
        public List<Speaker> Speakers { get; set; }

        [JsonProperty("sessions")]
        public List<Session> Sessions { get; set; }

        [JsonProperty("rooms")]
        public List<Room> Rooms { get; set; }

        [JsonProperty("sponsors")]
        public List<Sponsor> Sponsors { get; set; }
    }
}