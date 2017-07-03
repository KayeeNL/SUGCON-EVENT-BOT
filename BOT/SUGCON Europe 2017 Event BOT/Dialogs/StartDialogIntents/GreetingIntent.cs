using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using SUGCON.Event.Bot.Dialogs.Options;
using SUGCON.Event.Bot.LUIS;

namespace SUGCON.Event.Bot.Dialogs
{
    public partial class StartDialog
    {
        private async Task GreetingPromptAsync(IDialogContext context, IAwaitable<StartDialogOptions> argument)
        {
            _startDialogOptions = await argument;

            if (_startDialogOptions == StartDialogOptions.Agenda)
            {
                await context.PostAsync("For SUGCON Europe 2017, we have got the best agenda ever for you!");
                PromptDialog.Choice(context, AgendaPromptAsync, Enum.GetValues(typeof (AgendaDialogOptions)).Cast<AgendaDialogOptions>().ToArray(), "Please select a date:", "I didn't understand. Please choose one of the options.");
            }
            if (_startDialogOptions == StartDialogOptions.Sessions)
            {
                await context.PostAsync("We have some amazing sessions at SUGCON Europe 2017!");
                await PromptUserOnSessionSelection(context);
            }
            if (_startDialogOptions == StartDialogOptions.Speakers)
            {
                await context.PostAsync("We have the best speakers from Sitecore & the community for you!");
                await context.PostAsync("Just type in the name of the speaker you want to see session information for");
            }
            if (_startDialogOptions == StartDialogOptions.Sponsors)
            {
                await AskSponsorOptions(context);
            }
            if (_startDialogOptions == StartDialogOptions.Other)
            {
                await context.PostAsync("Okay, just type in the name of the speaker, or topic that you want to see session information for...or something completely random...");
            }
        }

        [LuisIntent(LuisIntents.Greeting)]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Hi there, welcome to the SUGCON Europe 2017 Bot!");
            await context.PostAsync("Experience the power of the Sitecore community");

            PromptDialog.Choice(context, GreetingPromptAsync, Enum.GetValues(typeof (StartDialogOptions)).Cast<StartDialogOptions>().ToArray(), "Please make a selection of what you're interested in:", "I didn't understand. Please choose one of the options.");
        }
    }
}