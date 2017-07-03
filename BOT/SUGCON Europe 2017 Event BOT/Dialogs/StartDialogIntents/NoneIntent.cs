using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace SUGCON.Event.Bot.Dialogs
{
    public partial class StartDialog
    {
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, but we don't understand you. I'm constantly learning. Please type in something new...");
        }
    }
}