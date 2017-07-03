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
        [LuisIntent(LuisIntents.AkshaySura)]
        public async Task Akshay(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("There can only be one answer to what you're asking! The look-a-like of MC Hammer...");
            var message = context.MakeMessage();
            var personsList = new List<HeroCardInformation>();
            var akshay = CreateHeroCardPersonInformation("Akshay 'Why can't you be my friend' Sura", string.Empty, "https://www.sugcon.eu/wp-content/uploads/2017/05/akshaysura-rap.jpg", string.Empty, string.Empty);
            personsList.Add(akshay);
            message.AddAsHeroCard(personsList);
            await context.PostAsync(message);
        }
    }
}