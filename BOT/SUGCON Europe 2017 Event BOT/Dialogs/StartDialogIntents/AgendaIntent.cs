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
        private AgendaDialogOptions _agendaDialogOptions;

        private async Task AgendaPromptAsync(IDialogContext context, IAwaitable<AgendaDialogOptions> argument)
        {
            _agendaDialogOptions = await argument;
            if (_agendaDialogOptions == AgendaDialogOptions.Day1)
            {
                var sessions = SessionService.GetSessionsByDate(new DateTime(2017, 05, 18));
                var message = context.MakeMessage();
                message.AddSessionsAsHeroCards(sessions);
                await context.PostAsync(message);
            }
            if (_agendaDialogOptions == AgendaDialogOptions.Day2)
            {
                var sessions = SessionService.GetSessionsByDate(new DateTime(2017, 05, 19));
                var message = context.MakeMessage();
                message.AddSessionsAsHeroCards(sessions);
                await context.PostAsync(message);
            }
        }

        [LuisIntent(LuisIntents.AgendaInformation)]
        public async Task AgendaInformation(IDialogContext context, LuisResult result)
        {
            if (!result.Entities.Any())
            {
                PromptDialog.Choice(context, AgendaPromptAsync, Enum.GetValues(typeof (AgendaDialogOptions)).Cast<AgendaDialogOptions>().ToArray(), "Please select a date:", "I didn't understand. Please choose one of the options.");
            }
            EntityRecommendation entityRecommendation;

            if (result.TryFindEntity(LuisEntities.Day, out entityRecommendation))
            {
                if (entityRecommendation.Entity.ToLowerInvariant().Contains("day 1"))
                {
                    await ShowSessionsOfDay1(context);
                }
                if (entityRecommendation.Entity.ToLowerInvariant().Contains("day 2"))
                {
                    await ShowSessionsOfDay2(context);
                }
            }
        }

        private async Task ResumeSessionsByDatePrompt(IDialogContext context, IAwaitable<AgendaDialogOptions> result)
        {
            var dateOption = await result;

            if (dateOption == AgendaDialogOptions.Day1)
            {
                await ShowSessionsOfDay1(context);
            }
            if (dateOption == AgendaDialogOptions.Day2)
            {
                await ShowSessionsOfDay2(context);
            }
        }

        private async Task ShowSessionsOfDay2(IDialogContext context)
        {
            var sessions = SessionService.GetSessionsByDate(new DateTime(2017, 05, 19));
            var message = context.MakeMessage();
            message.AddSessionsAsHeroCards(sessions);
            await context.PostAsync(message);
        }

        private async Task ShowSessionsOfDay1(IDialogContext context)
        {
            var sessions = SessionService.GetSessionsByDate(new DateTime(2017, 05, 18));
            var message = context.MakeMessage();
            message.AddSessionsAsHeroCards(sessions);
            await context.PostAsync(message);
        }
    }
}