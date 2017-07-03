using System.Collections.Generic;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Services
{
    public interface ISpeakerService
    {
        List<Speaker> GetAllSpeakers();
        Speaker GetSpeakerInformation(int speakerId);
        Speaker GetSpeakerInformationByName(string name);
        List<Speaker> GetSpeakersFromCompany(string companyName);
    }
}