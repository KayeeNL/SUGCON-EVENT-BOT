using System;
using System.Collections.Generic;
using System.Linq;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Services
{
    [Serializable]
    public class SessionService : ServiceBase, ISessionService
    {
        private const string DateCompareFormat = "yy-MM-dd";

        private readonly ISpeakerService _speakerService;

        public SessionService(ISpeakerService speakerService)
        {
            _speakerService = speakerService;
        }

        public List<Session> GetSessionsByTopic(string topic)
        {
            return PopulateSessionsWithSpeakerInfo(SugconResponse.Sessions.Where(x => x.Title.Contains(topic) || x.Tags.Contains(topic)).ToList().OrderBy(x => x.From).ToList());
        }

        public List<Session> GetSessionsBySpeaker(int speakerId)
        {
            return PopulateSessionsWithSpeakerInfo(SugconResponse.Sessions.Where(session => session.Speakers.Any(speaker => speaker.Id == speakerId)).OrderBy(x => x.From).ToList());
        }

        public Session GetSessionInformation(int sessionId)
        {
            return SugconResponse.Sessions.FirstOrDefault(session => session.Id == sessionId);
        }

        public List<Session> GetSessionsByRoom(int roomId)
        {
            return PopulateSessionsWithSpeakerInfo(SugconResponse.Sessions.Where(x => x.Room == roomId).OrderBy(x => x.From).ToList());
        }

        public List<Session> GetSessionsByRoomOnDate(int roomId, DateTime date)
        {
            return PopulateSessionsWithSpeakerInfo(SugconResponse.Sessions.Where(x => x.Room == roomId && x.From.ToString(DateCompareFormat) == date.ToString(DateCompareFormat)).OrderBy(x => x.From).ToList());
        }

        public List<Session> GetSessionsByDate(DateTime date)
        {
            return PopulateSessionsWithSpeakerInfo(SugconResponse.Sessions.Where(session => session.From.ToString(DateCompareFormat) == date.ToString(DateCompareFormat)).OrderBy(x => x.From).ThenBy(x=> x.Room).ToList());
        }

        private List<Session> PopulateSessionsWithSpeakerInfo(List<Session> sessions)
        {
            foreach (var session in sessions)
            {
                foreach (var speaker in session.Speakers)
                {
                    var newSpeakerInfo = _speakerService.GetSpeakerInformation(speaker.Id);
                    speaker.Name = newSpeakerInfo.Name;
                    speaker.Company = newSpeakerInfo.Company;
                    speaker.ImageUrl = newSpeakerInfo.ImageUrl;
                }
            }
            return sessions;
        }
    }
}