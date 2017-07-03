using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;
using SUGCON.Event.Bot.Extensions;
using SUGCON.Event.Bot.LUIS;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Dialogs
{
    public partial class StartDialog
    {
        [LuisIntent(LuisIntents.OrganizationInformation)]
        public async Task Organization(IDialogContext context, LuisResult result)
        {
            var message = context.MakeMessage();
            var personsList = new List<HeroCardInformation>();
            var alan = CreateHeroCardPersonInformation("Alan Coates", "Technical Director, Sitecore MVP, partner - @Pentia", "https://www.sugcon.eu/wp-content/uploads/2016/04/alancoates.jpg", "https://www.linkedin.com/in/mrcoates", "https://twitter.com/TakeItEasyAlan");
            var marc = CreateHeroCardPersonInformation("Marc Duiker", "Lead consultant Azure & Sitecore, Sitecore MVP 2013-2016 - @Xpirit", "https://www.sugcon.eu/wp-content/uploads/2017/01/marcduiker.jpg", "https://nl.linkedin.com/in/mduiker", "https://twitter.com/marcduiker");
            var robin = CreateHeroCardPersonInformation("Robin Hermanussen", "Lead Sitecore/.NET developer & Sitecore MVP - @Lukkien", "https://www.sugcon.eu/wp-content/uploads/2016/04/robinhermanussen.jpg", "https://nl.linkedin.com/in/hermanussen", "https://twitter.com/knifecore");
            var robbert = CreateHeroCardPersonInformation("Robbert Hock", "Freelance Sitecore Technology Specialist, Sitecore MVP - @Kayee", "https://www.sugcon.eu/wp-content/uploads/2017/03/robberthock.jpg", "https://nl.linkedin.com/in/robberthock", "https://twitter.com/kayeeNL");
            var anders = CreateHeroCardPersonInformation("Anders Laub", "Senior Sitecore Specialist, Sitecore MVP - @LAUB + CO", "https://www.sugcon.eu/wp-content/uploads/2016/02/anderslaub.jpg", "https://dk.linkedin.com/in/anderslaub", "https://twitter.com/AndersLaub");
            var gert = CreateHeroCardPersonInformation("Gert Maas", "Sitecore MVP & Technology Consultant - @Macaw", "https://www.sugcon.eu/wp-content/uploads/2017/03/gertmaas.jpg", "https://www.linkedin.com/in/gertmaas/", "https://twitter.com/gertmaas");
            var akshay = CreateHeroCardPersonInformation("Akshay Sura", "WEM Technical capability leader / Sitecore MVP 2014-2017", "https://www.sugcon.eu/wp-content/uploads/2017/01/akshaysura.png", "https://www.linkedin.com/in/akshaysura", "https://twitter.com/akshaysura13");
            var thomas = CreateHeroCardPersonInformation("Thomas Stern", "Head of innovation, Senior Consultant & Sitecore MVP - @Pentia", "https://www.sugcon.eu/wp-content/uploads/2016/04/thomasstern.jpg", "https://www.linkedin.com/in/akshaysura", "https://twitter.com/akshaysura13");
            var reinoud = CreateHeroCardPersonInformation("Reinoud van Dalen", "Sitecore MVP & Senior Developer - @Valtech", "https://www.sugcon.eu/wp-content/uploads/2016/04/reinoudvandalen.jpeg", "https://nl.linkedin.com/in/reinoudvandalen", "https://twitter.com/RvDalen");
            var andy = CreateHeroCardPersonInformation("Andy van de Sande", "Lead CMS Strategist, Sitecore Exopert & Sitecore MVP - @Redhotminute", "https://www.sugcon.eu/wp-content/uploads/2016/04/andyvandesande.jpg", "https://nl.linkedin.com/in/andyvandesande", "https://twitter.com/andyvandesande");
            var pieter = CreateHeroCardPersonInformation("Pieter Brinkman", "Director Technical Marketing - @Sitecore", "https://www.sugcon.eu/wp-content/uploads/2016/02/pieterbrinkman-1.jpg", "https://nl.linkedin.com/in/pbrink", "https://twitter.com/pieterbrink123");
            var tamas = CreateHeroCardPersonInformation("Tamas Varga", "Senior Technical Evangelist - @Sitecore", "https://www.sugcon.eu/wp-content/uploads/2017/01/tamasvarga.jpg", "https://www.linkedin.com/in/vargat", "https://twitter.com/VargaT");

            personsList.Add(alan);
            personsList.Add(marc);
            personsList.Add(robin);
            personsList.Add(robbert);
            personsList.Add(anders);
            personsList.Add(gert);
            personsList.Add(akshay);
            personsList.Add(thomas);
            personsList.Add(reinoud);
            personsList.Add(andy);
            personsList.Add(pieter);
            personsList.Add(tamas);
            message.AddAsHeroCard(personsList);
            await context.PostAsync("Meet the great people that are responsible for organizing SUGCON Europe 2017");
            await context.PostAsync(message);
        }
    }
}