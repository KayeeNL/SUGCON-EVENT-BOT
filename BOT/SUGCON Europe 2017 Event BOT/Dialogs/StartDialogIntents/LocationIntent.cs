using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using SUGCON.Event.Bot.LUIS;

namespace SUGCON.Event.Bot.Dialogs
{
    public partial class StartDialog
    {
        [LuisIntent(LuisIntents.LocationInformation)]
        public async Task LocationInformation(IDialogContext context, LuisResult result)
        {
            var imageMessage = context.MakeMessage();
            var attachment = new Attachment
            {
                ContentType = "image/jpg",
                ContentUrl = "https://www.sugcon.eu/wp-content/uploads/2017/05/postillion-setup.jpg"
            };
            imageMessage.Attachments.Add(attachment);
            await context.PostAsync("SUGCON Europe 2017 will be held on May 18th and May 19th 2017 at the following location:");
            await context.PostAsync("Postillion Convention Centre Amsterdam");
            await context.PostAsync("Paul van Vlissingenstraat 8, 1096 BK, Amsterdam, The Netherlands");
            await context.PostAsync(imageMessage);
            var heroMessage = context.MakeMessage();
            var heroCard = new HeroCard
            {
                Title = "Postillion Convention Centre Amsterdam",
                Images = new List<CardImage>
                {
                    new CardImage("https://www.sugcon.eu/wp-content/uploads/2017/02/postillion-amsterdam-background-1024x480.jpg")
                },
                Buttons = new List<CardAction>
                {
                    new CardAction
                    {
                        Value = "https://www.sugcon.eu/location-venue/",
                        Type = "openUrl",
                        Title = "More info..."
                    }
                }
            };
            heroMessage.Attachments.Add(heroCard.ToAttachment());
            await context.PostAsync(heroMessage);
        }
    }
}