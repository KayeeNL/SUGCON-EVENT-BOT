using Microsoft.Bot.Builder.FormFlow;

namespace SUGCON.Event.Bot.Dialogs.Options
{
    public enum SessionOptions
    {
        [Describe("By room")] ByRoom = 1,
        [Describe("By speaker")] BySpeaker = 2,
        [Describe("By date")] ByDate = 3,
        [Describe("Search by topic")] ByTopic = 4
    }
}