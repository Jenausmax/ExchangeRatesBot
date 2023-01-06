using ExchangeRatesBot.Domain.Interfaces;
using ExchangeRatesBot.Domain.Models;
using Serilog;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeRatesBot.App.Services
{
    public class ProcessingService : IProcessingService
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public ProcessingService(IHttpClientFactory clientFactory,
            ILogger logger)
        {
            _client = clientFactory.CreateClient("client");
            _logger = logger;
        }

        public async Task<Root> RequestProcessingAsync(int day, string charCode, CancellationToken cancellationToken)
        {
            try
            {
                var resp = await _client.PostAsync($"?charcode={charCode}&day={day}", null, cancellationToken);
                var resultContent = await resp.Content.ReadAsStreamAsync();
                var res = await JsonSerializer.DeserializeAsync<Root>(resultContent, cancellationToken: cancellationToken);
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
