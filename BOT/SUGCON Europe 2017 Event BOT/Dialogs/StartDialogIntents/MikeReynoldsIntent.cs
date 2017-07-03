using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using SUGCON.Event.Bot.Extensions;
using SUGCON.Event.Bot.LUIS;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Dialogs
{
    public partial class StartDialog
    {
        [LuisIntent(LuisIntents.MikeReynolds)]
        public async Task MikeReynolds(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("There can only be one answer to what you're asking! Mr. Legend himself...");
            string[] images = {"https://www.sugcon.eu/wp-content/uploads/2017/05/mikereynolds_goldsuit2.jpg", "https://www.sugcon.eu/wp-content/uploads/2017/05/mikereynolds_goldsuit.jpg"};
            var selectedImage = result.Query.ToLowerInvariant().Contains("danc") ? "https://www.sugcon.eu/wp-content/uploads/2017/05/akshay-mike-dancing.jpg" : images[new Random().Next(0, images.Length)];
            var message = context.MakeMessage();
            var personsList = new List<HeroCardInformation>();
            var mike = CreateHeroCardPersonInformation("Mike 'Gold Suit' Reynolds", string.Empty, selectedImage, string.Empty, string.Empty);
            personsList.Add(mike);
            message.AddAsHeroCard(personsList);

            await context.PostAsync(message);
        }
    }
}