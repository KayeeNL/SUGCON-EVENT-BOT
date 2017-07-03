using System;
using System.Collections.Generic;
using System.Linq;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Services
{
    [Serializable]
    public class SponsorService : ServiceBase, ISponsorService
    {
        public List<Sponsor> GetAllSponsors()
        {
            return SugconResponse.Sponsors.OrderBy(x => x.Sponsortype == "Grande").ThenBy(x => x.Sponsorpackage == "Venti").ToList();
        }

        public List<Sponsor> GetSponsorsByType(string type)
        {
            return SugconResponse.Sponsors.Where(x => x.Sponsorpackage.ToLowerInvariant().Contains(type.ToLowerInvariant()) || x.Sponsortype.ToLowerInvariant().Contains(type.ToLowerInvariant())).OrderBy(x => x.Id).ToList();
        }
    }
}