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
        [LuisIntent(LuisIntents.AboutInformation)]
        public async Task About(IDialogContext context, LuisResult result)
        {
            var message = context.MakeMessage();
            var herocards = new List<HeroCardInformation>();
            var robbert = CreateHeroCardPersonInformation("Robbert Hock", "Freelance Sitecore Technology Specialist, Sitecore MVP - @Kayee", "https://www.sugcon.eu/wp-content/uploads/2017/03/robberthock.jpg", "https://nl.linkedin.com/in/robberthock", "https://twitter.com/kayeeNL");
            var alex = CreateHeroCardPersonInformation("Alex van Wolferen", "Senior Software Developer - @Suneco", "https://www.sugcon.eu/wp-content/uploads/2017/03/alexvanwolferen.jpg", "https://nl.linkedin.com/in/alexvanwolferen", "https://twitter.com/avwolferen");
            herocards.Add(robbert);
            herocards.Add(alex);
            message.AddAsHeroCard(herocards);
            await context.PostAsync("Meet the persons behind the SUGCON Europe 2017 bot");
            await context.PostAsync(message);
        }
    }
}