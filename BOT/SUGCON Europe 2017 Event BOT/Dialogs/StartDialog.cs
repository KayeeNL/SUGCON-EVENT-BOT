using System;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using SUGCON.Event.Bot.Dialogs.Options;
using SUGCON.Event.Bot.Models;
using SUGCON.Event.Bot.Services;

namespace SUGCON.Event.Bot.Dialogs
{
    [LuisModel("07b06373-9949-4b5e-b7a5-a3397fefb754", "e3089857072f4e7586a04940baf0e805")]
    [Serializable]
    public partial class StartDialog : LuisDialog<object>
    {
        private ISessionService _sessionService;
        private ISpeakerService _speakerService;
        private SponsorOptions _sponsorOptions;
        private ISponsorService _sponsorService;

        private StartDialogOptions _startDialogOptions;

        public ISponsorService SponsorService => _sponsorService ?? (_sponsorService = new SponsorService());
        public ISpeakerService SpeakerService => _speakerService ?? (_speakerService = new SpeakerService());
        public ISessionService SessionService => _sessionService ?? (_sessionService = new SessionService(SpeakerService));

        private static HeroCardInformation CreateHeroCardPersonInformation(string title, string subtitle, string imageurl, string linkedinUrl, string twitterUrl)
        {
            var heroCardPersonInformation = new HeroCardInformation
            {
                Title = title,
                Subtitle = subtitle,
                ImageUrl = imageurl
            };

            if (!string.IsNullOrEmpty(linkedinUrl))
            {
                var linkedinButton = new HeroCardButton
                {
                    ButtonText = "See LinkedIn profile",
                    ButtonUrl = linkedinUrl
                };
                heroCardPersonInformation.Buttons.Add(linkedinButton);
            }

            if (!string.IsNullOrEmpty(twitterUrl))
            {
                var twitterButton = new HeroCardButton
                {
                    ButtonText = "See Twitter profile",
                    ButtonUrl = twitterUrl
                };
                heroCardPersonInformation.Buttons.Add(twitterButton);
            }

            return heroCardPersonInformation;
        }
    }
}