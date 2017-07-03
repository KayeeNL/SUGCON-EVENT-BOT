using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using SUGCON.Event.Bot.Dialogs.Options;
using SUGCON.Event.Bot.Extensions;
using SUGCON.Event.Bot.LUIS;

namespace SUGCON.Event.Bot.Dialogs
{
    public partial class StartDialog
    {
        [LuisIntent(LuisIntents.SessionInformation)]
        public async Task GetSessionInformation(IDialogContext context, LuisResult result)
        {
            if (!result.Entities.Any())
            {
                await PromptUserOnSessionSelection(context);
            }
            EntityRecommendation entityRecommendation;

            if (result.TryFindEntity(LuisEntities.Topic, out entityRecommendation))
            {
                var sessions = SessionService.GetSessionsByTopic(entityRecommendation.Entity);
                if (sessions.Any())
                {
                    var reply = context.MakeMessage();
                    reply.AddSessionsAsHeroCards(sessions);
                    await context.PostAsync($"We have great sessions on {entityRecommendation.Entity}");
                    await context.PostAsync(reply);
                }
                else
                {
                    await context.PostAsync($"Sorry, but we don't understand you. We couldn't find any session on {entityRecommendation.Entity}, please search on another topic.");
                }
            }
            if (result.TryFindEntity(LuisEntities.SessionId, out entityRecommendation))
            {
                var session = SessionService.GetSessionInformation(Convert.ToInt32(entityRecommendation.Entity));
                await context.PostAsync($"{session.Summary}");
            }
            if (result.TryFindEntity(LuisEntities.Agenda, out entityRecommendation))
            {
                await context.PostAsync("For SUGCON Europe 2017, we have got the best agenda ever for you!");
                PromptDialog.Choice(context, AgendaPromptAsync, Enum.GetValues(typeof (AgendaDialogOptions)).Cast<AgendaDialogOptions>().ToArray(), "Please select a date:", "I didn't understand. Please choose one of the options.");
            }
        }

        private async Task PromptUserOnSessionSelection(IDialogContext context)
        {
            PromptDialog.Choice(context, ResumeSessionTypePrompt, Enum.GetValues(typeof (SessionOptions)).Cast<SessionOptions>().ToArray(), "Please make a selection of what sessions you want to have more information on:", "I didn't understand. Please choose one of the options.");
        }

        private async Task ResumeSessionTypePrompt(IDialogContext context, IAwaitable<SessionOptions> result)
        {
            var sessionOption = await result;

            if (sessionOption == SessionOptions.ByRoom)
            {
                PromptDialog.Choice(context, ResumeSessionsByRoomPrompt, Enum.GetValues(typeof (RoomOptions)).Cast<RoomOptions>().ToArray(), "Please select a room you want to see the sessions for:", "I didn't understand. Please choose one of the options.");
            }
            if (sessionOption == SessionOptions.ByDate)
            {
                PromptDialog.Choice(context, ResumeSessionsByDatePrompt, Enum.GetValues(typeof (AgendaDialogOptions)).Cast<AgendaDialogOptions>().ToArray(), "Please select a date you want to see the sessions for:", "I didn't understand. Please choose one of the options.");
            }
            if (sessionOption == SessionOptions.BySpeaker)
            {
                await AskOpenSpeakerQuestion(context);
            }
            if (sessionOption == SessionOptions.ByTopic)
            {
                await context.PostAsync("We have some amazing sessions at SUGCON Europe 2017");
                await context.PostAsync("Just let us know what topic you are interested in...");
            }
        }

        private async Task ResumeSessionsByRoomPrompt(IDialogContext context, IAwaitable<RoomOptions> result)
        {
            var roomOption = await result;
            var sessions = SessionService.GetSessionsByRoom((int) roomOption);
            var message = context.MakeMessage();
            message.AddSessionsAsHeroCards(sessions);
            await context.PostAsync(message);
        }
    }
}