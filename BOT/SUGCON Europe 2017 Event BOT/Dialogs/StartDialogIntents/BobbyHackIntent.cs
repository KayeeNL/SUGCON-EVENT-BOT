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
        [LuisIntent(LuisIntents.BobbyHack)]
        public async Task BobbyHack(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Bobby Hack might attend SUGCON Europe 2017. Make sure you stay off the WIFI, and make sure you don't get PWNED by Bobby Hack.");
            var message = context.MakeMessage();
            var heroCardList = new List<HeroCardInformation>();
            var bobby = CreateHeroCardPersonInformation("'Bobby Hack' - Look out for this guy! ", string.Empty, "https://www.sugcon.eu/wp-content/uploads/2017/05/bobbyhack.jpg", string.Empty, string.Empty);
            heroCardList.Add(bobby);
            message.AddAsHeroCard(heroCardList);
            await context.PostAsync(message);
        }
    }
}