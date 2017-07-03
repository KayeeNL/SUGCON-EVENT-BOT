using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.Bot.Builder.Dialogs;

namespace SUGCON.Event.Bot.Dialogs
{
    [Serializable]
    public class ExceptionHandlerDialog<T> : IDialog<object>
    {
        private readonly IDialog<T> _dialog;
        private readonly bool _displayException;
        private readonly int _stackTraceLength;

        [NonSerialized] private readonly TelemetryClient _telemetryClient = new TelemetryClient();
        public ExceptionHandlerDialog(IDialog<T> dialog, bool displayException, int stackTraceLength = 500)
        {
            _dialog = dialog;
            _displayException = displayException;
            _stackTraceLength = stackTraceLength;
        }

        public async Task StartAsync(IDialogContext context)
        {
            try
            {
                context.Call(_dialog, ResumeAsync);
            }
            catch (Exception e)
            {
                //add logging here
                var messages = new Dictionary<string, string>();
                messages.Add("Exception", e.Message);
                messages.Add("Stacktrace", e.StackTrace);
                _telemetryClient.TrackEvent("BotException", messages);

                if (_displayException)
                    await DisplayException(context, e).ConfigureAwait(false);
            }
        }

        private async Task ResumeAsync(IDialogContext context, IAwaitable<T> result)
        {
            try
            {
                context.Done(await result);
            }
            catch (Exception e)
            {
                //add logging here
                var messages = new Dictionary<string, string>();
                messages.Add("Exception", e.Message);
                messages.Add("Stacktrace", e.StackTrace);
                _telemetryClient.TrackEvent("BotException", messages);

                if (_displayException)
                    await DisplayException(context, e).ConfigureAwait(false);
            }
        }

        private async Task DisplayException(IDialogContext context, Exception e)
        {
            var stackTrace = e.StackTrace;
            if (stackTrace.Length > _stackTraceLength)
                stackTrace = stackTrace.Substring(0, _stackTraceLength) + "…";
            stackTrace = stackTrace.Replace(Environment.NewLine, "  \n");

            var message = e.Message.Replace(Environment.NewLine, "  \n");

            var exceptionStr = $"**{message}**  \n\n{stackTrace}";

            await context.PostAsync(exceptionStr).ConfigureAwait(false);
        }
    }
}