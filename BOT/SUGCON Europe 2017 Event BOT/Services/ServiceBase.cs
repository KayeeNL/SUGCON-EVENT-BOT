using System;
using System.Net;
using Newtonsoft.Json;
using SUGCON.Event.Bot.Models;

namespace SUGCON.Event.Bot.Services
{
    [Serializable]
    public abstract class ServiceBase
    {
        private SugconResponse _response;

        public SugconResponse SugconResponse
        {
            get
            {
                if (_response == null)
                {
                    var webClient = new WebClient();
                    var jsonResponse = webClient.DownloadString(@"https://raw.githubusercontent.com/KayeeNL/SUGCON-EVENT-BOT/master/Documentation/sugconeurope2017schedule.json");
                    try
                    {
                        _response = JsonConvert.DeserializeObject<SugconResponse>(jsonResponse);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                }

                return _response;
            }
        }
    }
}