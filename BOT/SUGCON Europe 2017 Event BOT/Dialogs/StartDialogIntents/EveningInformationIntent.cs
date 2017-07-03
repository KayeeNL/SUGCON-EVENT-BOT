using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using SUGCON.Event.Bot.Dialogs.Options;
using SUGCON.Event.Bot.Extensions;
using SUGCON.Event.Bot.LUIS;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Dialogs
{
    public partial class StartDialog
    {
        [LuisIntent(LuisIntents.EveningInformation)]
        public async Task EveningInformation(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("We have evening activities on May 18th and May 19th.");
            PromptDialog.Choice(context, EveningInformationPromptAsync, Enum.GetValues(typeof (EveningInformationOptions)).Cast<EveningInformationOptions>().ToArray(), "Please make a selection of what you're interested in:", "I didn't understand. Please choose one of the options.");
        }

        private async Task EveningInformationPromptAsync(IDialogContext context, IAwaitable<EveningInformationOptions> result)
        {
            var eveningInformationOptions = await result;

            if (eveningInformationOptions == EveningInformationOptions.MVPAwards)
            {
                await context.PostAsync("A Sitecore MVP is an individual with expertise in Sitecore who actively participate in online and offline communities to share their knowledge and expertise with other Sitecore partners and customers. During SUGCON Europe 2017, we will celebrate the 2017 MVP’s and all attending MVP's will receive their MVP award.");
                var heroCardMessage = context.MakeMessage();
                var heroCardList = new List<HeroCardInformation>();
                var bobby = CreateHeroCardPersonInformation("Sitecore MVP Award Ceremony", string.Empty, "https://www.sugcon.eu/wp-content/uploads/2017/02/sitecore-mvp-awards-2016-001.jpg", string.Empty, string.Empty);
                heroCardList.Add(bobby);
                heroCardMessage.AddAsHeroCard(heroCardList);
                await context.PostAsync(heroCardMessage);
            }
            if (eveningInformationOptions == EveningInformationOptions.HackathonAwards)
            {
                await context.PostAsync("The 2017 Sitecore Hackathon is a free online community driven event organized by Akshay Sura and supported by Sitecore and was held on March 3, 2017. During SUGCON Europe 2017, we will celebrate the 2017 Hackathon winners.");
                var heroCardMessage = context.MakeMessage();
                var heroCardList = new List<HeroCardInformation>();
                var bobby = CreateHeroCardPersonInformation("Sitecore Hackathon 2017 ceremony", string.Empty, "https://www.sugcon.eu/wp-content/uploads/2017/02/sitecore-hackathon-logo.png", string.Empty, string.Empty);
                heroCardList.Add(bobby);
                heroCardMessage.AddAsHeroCard(heroCardList);
                await context.PostAsync(heroCardMessage);
            }
            if (eveningInformationOptions == EveningInformationOptions.AfterParty)
            {
                await context.PostAsync("Dutch Sitecore Partner Mirabeau is hosting an after party on Friday May 19th, straight after SUGCON Europe 2017 closes.");
                await context.PostAsync("Enjoy some drinks and snacks and connect a little bit longer with the Sitecore community");
                var message = context.MakeMessage();
                var card = new ThumbnailCard();
                card.Title = "After Party SUGCON Europe 2017 ";
                card.Subtitle = "Drinks, Snacks and more networking";
                card.Images = new List<CardImage>();
                var image = new CardImage("https://www.sugcon.eu/wp-content/uploads/2017/05/mirabeau-office.jpg", "Mirabeau");
                card.Images.Add(image);
                message.Attachments.Add(card.ToAttachment());
                await context.PostAsync(message);
                await context.PostAsync("Paul van Vlissingenstraat 10C, 1096 BK Amsterdam, right next to the SUGCON Venue!!!");
            }
            if (eveningInformationOptions == EveningInformationOptions.DinnerAndDrinks)
            {
                await context.PostAsync("On Thursday May 18th we have a buffet dinner, (from 18.00 hours on), with drinks and some fun entertainment. Connect with your peers, relax and have fun.");
            }
        }
    }
}