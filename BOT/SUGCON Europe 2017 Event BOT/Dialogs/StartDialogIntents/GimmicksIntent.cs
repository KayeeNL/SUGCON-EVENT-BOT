using System.Net;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using Newtonsoft.Json;
using SUGCON.Event.Bot.Extensions;
using SUGCON.Event.Bot.LUIS;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Dialogs
{
    public partial class StartDialog
    {
        [LuisIntent(LuisIntents.Gimmicks)]
        public async Task Gimmicks(IDialogContext context, LuisResult result)
        {
            if (result.Query.ToLowerInvariant().Contains("joke") || result.Query.ToLowerInvariant().Contains("fun"))
            {
                var client = new WebClient();
                var json = client.DownloadString("https://api.icndb.com/jokes/random?limitTo=[nerdy]");
                var response = JsonConvert.DeserializeObject<JokeResponse>(json);
                if (response != null)
                {
                    await context.PostAsync("Of course I know jokes! You love Chuck Norris as we do :-) ?");
                    var message = context.MakeMessage();
                    message.AddJokeAsHeroCard(response.Value.Joke);
                    await context.PostAsync(message);
                }
            }
        }
    }
}