using ExchangeRatesBot.Configuration.ModelConfig;
using ExchangeRatesBot.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace ExchangeRatesBot.App.Services
{
    public class ApiClientService : IApiClient
    {
        public HttpClient Client { get; set; }

        public ApiClientService(IOptions<BotConfig> config)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(config.Value.UrlRequest);
        }
    }
}
