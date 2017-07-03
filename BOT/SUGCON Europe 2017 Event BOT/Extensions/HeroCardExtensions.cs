using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Connector;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Extensions
{
    public static class HeroCardExtensions
    {
        public static void AddHeroCard<T>(this IMessageActivity message, string title, string subtitle, IEnumerable<T> options, IEnumerable<string> images = default(IEnumerable<string>))
        {
            var heroCard = GenerateHeroCard(title, subtitle, options, images);

            if (message.Attachments == null)
            {
                message.Attachments = new List<Attachment>();
            }

            message.Attachments.Add(heroCard.ToAttachment());
        }

        public static void AddHeroCard(this IMessageActivity message, string title, string subtitle, IList<KeyValuePair<string, string>> options, IEnumerable<string> images = default(IEnumerable<string>))
        {
            var heroCard = GenerateHeroCard(title, subtitle, options, images);

            if (message.Attachments == null)
            {
                message.Attachments = new List<Attachment>();
            }

            message.Attachments.Add(heroCard.ToAttachment());
        }

        public static void AddSessionsAsHeroCards(this IMessageActivity message, List<Session> sessions)
        {
            // Skype doesn't seem to handle the carousel attachmentlayout correctly when having multiple attachments
            message.AttachmentLayout = message.ChannelId == "skype" ? AttachmentLayoutTypes.List : AttachmentLayoutTypes.Carousel;

            foreach (var session in sessions)
            {
                var speakersTitle = string.Empty;
                for (var i = 0; i < session.Speakers.Count; i++)
                {
                    speakersTitle += session.Speakers[i].Name;
                    if (i != session.Speakers.Count - 1)
                    {
                        speakersTitle += " - ";
                    }
                }

                var card = new HeroCard
                {
                    Title = $"{session.From.ToShortDateString()} ({session.From.ToString("HH:mm")}-{session.Until.ToString("HH:mm")}) - {session.Title} - [{speakersTitle}]",
                    Subtitle = session.Summary
                };
                var cardImages = new List<CardImage>();
                if (!string.IsNullOrEmpty(session.SpeakersImage))
                {
                    cardImages.Add(new CardImage(session.SpeakersImage));
                }
                else
                {
                    cardImages = session.Speakers.Select(speaker => new CardImage(speaker.ImageUrl, speaker.Name)).ToList();
                }

                card.Images = cardImages;
                var cardButtons = new List<CardAction>();
                var cardAction = new CardAction
                {
                    Value = $"Show me session info for session {session.Id}",
                    Type = "postBack",
                    Title = "More info"
                };
                cardButtons.Add(cardAction);
                card.Buttons = cardButtons;
                var attachment = card.ToAttachment();
                message.Attachments.Add(attachment);
            }
        }

        public static void AddSpeakersAsHeroCards(this IMessageActivity message, List<Speaker> speakers)
        {
            // Skype doesn't seem to handle the carousel attachmentlayout correctly when having multiple attachments
            message.AttachmentLayout = message.ChannelId == "skype" ? AttachmentLayoutTypes.List : AttachmentLayoutTypes.Carousel;
            foreach (var speaker in speakers)
            {
                var card = new HeroCard {Title = $"{speaker.Name} - {speaker.Company}"};
                var speakerImage = new CardImage
                {
                    Alt = speaker.Name,
                    Url = speaker.ImageUrl
                };
                card.Images = new List<CardImage> {speakerImage};
                var cardButtons = new List<CardAction>();
                var cardAction = new CardAction
                {
                    Value = $"what is {speaker.Name} speaking on?",
                    Type = "postBack",
                    Title = "Session info"
                };
                cardButtons.Add(cardAction);
                card.Buttons = cardButtons;
                var attachment = card.ToAttachment();
                message.Attachments.Add(attachment);
            }
        }

        public static void AddJokeAsHeroCard(this IMessageActivity message, string joke)
        {
            message.AttachmentLayout = AttachmentLayoutTypes.List;
            var card = new HeroCard(subtitle: joke);
            var speakerImage = new CardImage
            {
                Url = "https://imgflip.com/s/meme/Chuck-Norris-Approves.jpg"
            };
            card.Images = new List<CardImage> {speakerImage};

            var attachment = card.ToAttachment();
            message.Attachments.Add(attachment);
        }

        public static void AddAsHeroCard(this IMessageActivity message, List<HeroCardInformation> herocards)
        {
            // Skype doesn't seem to handle the carousel attachmentlayout correctly when having multiple attachments
            message.AttachmentLayout = message.ChannelId == "skype" ? AttachmentLayoutTypes.List : AttachmentLayoutTypes.Carousel;
            foreach (var heroCardInformation in herocards)
            {
                var card = new HeroCard
                {
                    Title = heroCardInformation.Title,
                    Subtitle = heroCardInformation.Subtitle
                };
                var images = new List<CardImage>();
                var cardImage = new CardImage
                {
                    Url = heroCardInformation.ImageUrl,
                    Alt = heroCardInformation.Title
                };
                images.Add(cardImage);
                card.Images = images;
                var cardButtons = heroCardInformation.Buttons.Select(heroCardPersonButton => new CardAction
                {
                    Type = "openUrl", Value = heroCardPersonButton.ButtonUrl, Title = heroCardPersonButton.ButtonText
                }).ToList();

                card.Buttons = cardButtons;
                var attachment = card.ToAttachment();
                message.Attachments.Add(attachment);
            }
        }

        private static HeroCard GenerateHeroCard(string title, string subtitle, IEnumerable<KeyValuePair<string, string>> options, IEnumerable<string> images)
        {
            var actions = options.Select(option => new CardAction
            {
                Title = option.Key, Type = ActionTypes.ImBack, Value = option.Value
            }).ToList();

            var cardImages = new List<CardImage>();

            if (images != default(IEnumerable<string>))
            {
                cardImages.AddRange(images.Select(image => new CardImage
                {
                    Url = image
                }));
            }

            return new HeroCard(title, subtitle, images: cardImages, buttons: actions);
        }

        private static HeroCard GenerateHeroCard<T>(string title, string subtitle, IEnumerable<T> options, IEnumerable<string> images)
        {
            return GenerateHeroCard(title, subtitle, options.Select(option => new KeyValuePair<string, string>(option.ToString(), option.ToString())), images);
        }
    }
}