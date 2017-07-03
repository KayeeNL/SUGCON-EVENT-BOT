using System;
using System.Collections.Generic;
using System.Linq;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Services
{
    [Serializable]
    public class SpeakerService : ServiceBase, ISpeakerService
    {
        public List<Speaker> GetAllSpeakers()
        {
            return SugconResponse.Speakers;
        }

        public Speaker GetSpeakerInformation(int speakerId)
        {
            return SugconResponse.Speakers.FirstOrDefault(x => x.Id == speakerId);
        }

        public Speaker GetSpeakerInformationByName(string name)
        {
            return SugconResponse.Speakers.FirstOrDefault(x => x.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));
        }

        public List<Speaker> GetSpeakersFromCompany(string companyName)
        {
            return SugconResponse.Speakers.Where(x => x.Company.ToLowerInvariant().Contains(companyName.ToLowerInvariant())).ToList();
        }
    }
}