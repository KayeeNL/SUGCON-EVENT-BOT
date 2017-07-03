using System;
using Newtonsoft.Json;

namespace SUGCON.Event.Bot.Models
{
    [Serializable]
    public class Sponsor
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("companyname")]
        public string Companyname { get; set; }

        [JsonProperty("companydescripion")]
        public string Companydescripion { get; set; }

        [JsonProperty("companylogo")]
        public string Companylogo { get; set; }

        [JsonProperty("websiteurl")]
        public string Websiteurl { get; set; }

        [JsonProperty("sponsorpackage")]
        public string Sponsorpackage { get; set; }

        [JsonProperty("sponsortype")]
        public string Sponsortype { get; set; }
    }
}