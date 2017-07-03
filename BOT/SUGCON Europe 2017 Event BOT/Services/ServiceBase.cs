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
                    var jsonResponse = webClient.DownloadString(@"");
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