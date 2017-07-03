using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using SUGCON.Event.Bot.Extensions;
using SUGCON.Event.Bot.LUIS;

namespace SUGCON.Event.Bot.Dialogs
{
    public partial class StartDialog
    {
        [LuisIntent(LuisIntents.SpeakerInformation)]
        public async Task SpeakerInformation(IDialogContext context, LuisResult result)
        {
            if (!result.Entities.Any())
            {
                // Make an exception for Alex, since for some reason the phraselist isn't picked for his name
                if (result.Query.ToLowerInvariant() == "alex van wolferen")
                {
                    await HandleSpeakerHeroCard(context, result.Query);
                }
                else
                {
                    await ShowAllSpeakers(context, result.Query);
                }
            }
            else
            {
                EntityRecommendation entityRecommendation;

                if (result.TryFindEntity(LuisEntities.Speaker, out entityRecommendation))
                {
                    var speakerName = entityRecommendation.Entity;
                    await HandleSpeakerHeroCard(context, speakerName);
                }
                if (result.TryFindEntity(LuisEntities.Company, out entityRecommendation))
                {
                    var companyName = entityRecommendation.Entity;
                    var speakers = SpeakerService.GetSpeakersFromCompany(entityRecommendation.Entity);
                    if (speakers != null && speakers.Any())
                    {
                        var message = context.MakeMessage();
                        message.AddSpeakersAsHeroCards(speakers);
                        await context.PostAsync(message);
                    }
                    else
                    {
                        await context.PostAsync($"Sorry, but we don't understand you. We couldn't find speakers from a company called {companyName}");
                    }
                }
            }
        }

        private async Task HandleSpeakerHeroCard(IDialogContext context, string speakerName)
        {
            var speaker = SpeakerService.GetSpeakerInformationByName(speakerName);
            if (speaker != null)
            {
                var sessions = SessionService.GetSessionsBySpeaker(speaker.Id);
                if (sessions.Count > 0)
                {
                    var message = context.MakeMessage();
                    message.AddSessionsAsHeroCards(sessions);
                    await context.PostAsync(message);
                }
                else
                {
                    await ShowAllSpeakers(context, speakerName);
                }
            }
            else
            {
                await context.PostAsync($"Sorry, but we don't understand you. We couldn't find a speaker with name {speakerName}");
            }
        }

        private async Task ShowAllSpeakers(IDialogContext context, string speakerName)
        {
            await context.PostAsync($"Hi, we couldn't find any speaker by the name of {speakerName}. Here's the list of speakers...");
            var speakers = SpeakerService.GetAllSpeakers();
            var message = context.MakeMessage();
            message.AddSpeakersAsHeroCards(speakers);
            await context.PostAsync(message);
        }

        private static async Task AskOpenSpeakerQuestion(IDialogContext context)
        {
            await context.PostAsync("We have the best speakers from Sitecore & the community for you!");
            await context.PostAsync("Just type in the name of the speaker you want to see session information for...");
        }
    }
}