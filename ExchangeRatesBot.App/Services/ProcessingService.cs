using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ExchangeRatesBot.Configuration.ModelConfig;
using ExchangeRatesBot.Domain.Interfaces;
using ExchangeRatesBot.Domain.Models;
using Microsoft.Extensions.Options;
using Serilog;

namespace ExchangeRatesBot.App.Services
{
    public class ProcessingService
    {
        private readonly IApiClient _client;
        private readonly IOptions<BotConfig> _config;
        private readonly ILogger _logger;

        public ProcessingService(IApiClient apiClient,
            IOptions<BotConfig> config,
            ILogger logger)
        {
            _client = apiClient;
            _config = config;
            _logger = logger;
        }

        public async Task<Valute> RequestProcessing(CancellationToken cancel)
        {
            try
            {
                var resp = await _client.Client.PostAsync("", new StringContent(""));
                var resultContent = await resp.Content.ReadAsStreamAsync();
                var res = await JsonSerializer.DeserializeAsync<Valute>(resultContent);
                _logger.Information("Deserialize succes");
                return res;
            }
            catch (Exception e)
            {
                _logger.Error(e, $"Error: Deserialize {typeof(ProcessingService)}");
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
