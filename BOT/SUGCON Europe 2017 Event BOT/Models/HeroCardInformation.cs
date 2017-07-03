using System.Collections.Generic;

namespace SUGCON.Event.Bot.Models
{
    public class HeroCardInformation
    {
        public HeroCardInformation()
        {
            Buttons = new List<HeroCardButton>();
        }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ImageUrl { get; set; }

        public List<HeroCardButton> Buttons { get; set; }
    }
}