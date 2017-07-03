using System.Collections.Generic;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Services
{
    public interface ISponsorService
    {
        List<Sponsor> GetAllSponsors();
        List<Sponsor> GetSponsorsByType(string type);
    }
}