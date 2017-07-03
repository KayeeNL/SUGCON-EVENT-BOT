using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using SUGCON.Event.Bot.LUIS;

namespace SUGCON.Event.Bot.Dialogs
{
    public partial class StartDialog
    {
        [LuisIntent(LuisIntents.ClassifiedInformation)]
        public async Task ClassifiedInformation(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, but I can't answer that due to highly secretness from the Sitecore labs");
            await context.PostAsync("If I would tell you, i really have to ... you ;-)");
        }
    }
}