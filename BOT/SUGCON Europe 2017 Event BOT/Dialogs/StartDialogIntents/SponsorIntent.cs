using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using SUGCON.Event.Bot.Dialogs.Options;
using SUGCON.Event.Bot.Extensions;
using SUGCON.Event.Bot.LUIS;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Dialogs
{
    public partial class StartDialog
    {
        [LuisIntent(LuisIntents.SponsorInformation)]
        public async Task Sponsor(IDialogContext context, LuisResult result)
        {
            if (!result.Entities.Any())
            {
                await AskSponsorOptions(context);
            }
            else
            {
                EntityRecommendation entityRecommendation;

                if (result.TryFindEntity(LuisEntities.SponsorType, out entityRecommendation))
                {
                    var sponsortype = entityRecommendation.Entity;
                    await context.PostAsync($"We have some great {sponsortype} sponsors at SUGCON Europe 2017!");
                    await HandleSponsorsHeroCards(context, sponsortype);
                }
            }
        }

        private async Task AskSponsorOptions(IDialogContext context)
        {
            await context.PostAsync("Thanks to our amazing sponsors we can make it possible to have the best European Sitecore event in 2017!");
            PromptDialog.Choice(context, SponsorPromptAsync, Enum.GetValues(typeof (SponsorOptions)).Cast<SponsorOptions>().ToArray(), "Please make a selection which type of sponsor you want to have information for.");
        }

        private async Task SponsorPromptAsync(IDialogContext context, IAwaitable<SponsorOptions> result)
        {
            _sponsorOptions = await result;
            var sponsorType = string.Empty;
            switch (_sponsorOptions)
            {
                case SponsorOptions.Grande:
                    sponsorType = "Grande";
                    break;
                case SponsorOptions.Venti:
                    sponsorType = "Venti";
                    break;
                case SponsorOptions.CommunityPlus:
                    sponsorType = "Community Plus";
                    break;
                case SponsorOptions.Community:
                    sponsorType = "Community";
                    break;
                case SponsorOptions.LanyardsAndBadges:
                    sponsorType = "Lanyards & Badges";
                    break;
                case SponsorOptions.DelegateBags:
                    sponsorType = "Delegate bags";
                    break;
                case SponsorOptions.Dinner:
                    sponsorType = "Dinner";
                    break;
                case SponsorOptions.Commercial:
                    sponsorType = "Commercial";
                    break;
            }

            await HandleSponsorsHeroCards(context, sponsorType);
        }

        private async Task HandleSponsorsHeroCards(IDialogContext context, string sponsorType)
        {
            var sponsors = SponsorService.GetSponsorsByType(sponsorType);
            if (sponsors.Any())
            {
                var message = context.MakeMessage();
                var herocards = sponsors.Select(sponsor => CreateHeroCardSponsorInformation(sponsor.Companyname, sponsor.Companydescripion, sponsor.Companylogo, sponsor.Websiteurl)).ToList();
                message.AddAsHeroCard(herocards);
                //await context.PostAsync($"We have some great {sponsorType} sponsors at SUGCON Europe 2017!");
                await context.PostAsync(message);
            }
        }

        private static HeroCardInformation CreateHeroCardSponsorInformation(string title, string subtitle, string imageurl, string websiteUrl)
        {
            var heroCardInformation = new HeroCardInformation
            {
                Title = title,
                Subtitle = subtitle,
                ImageUrl = imageurl
            };

            if (!string.IsNullOrEmpty(websiteUrl))
            {
                var linkedinButton = new HeroCardButton
                {
                    ButtonText = "See website",
                    ButtonUrl = websiteUrl
                };
                heroCardInformation.Buttons.Add(linkedinButton);
            }

            return heroCardInformation;
        }
    }
}