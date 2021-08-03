using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ExchangeRatesBot.Domain.Interfaces;
using ExchangeRatesBot.Domain.Models.GetModels;
using Serilog;

namespace ExchangeRatesBot.App.Services
{
    public class MessageValuteService : IMessageValute
    {
        private readonly IProcessingService _processingService;
        private readonly ILogger _logger;

        public MessageValuteService(IProcessingService processingService, ILogger logger)
        {
            _processingService = processingService;
            _logger = logger;
        }

        public async Task<string> GetValuteMessage(int day, string charCode, CancellationToken cancel)
        {
            var valutesRoot = _processingService.RequestProcessing(day, charCode, cancel);

            if (valutesRoot == null)
            {
                _logger.Error($"Type error: {typeof(MessageValuteService)}.: Collection null");
                return " ";
            }

            var getValutesModels = valutesRoot.Result.GetValuteModels;
            var valutes = new List<Valute>();
            var res = "";

            foreach (var item in getValutesModels)
            {
                
                valutes.Add(new Valute()
                {
                    CharCode = item.CharCode,
                    DateValute = item.DateValute,
                    Name = item.Name,
                    Value = item.Value
                });
            }

            foreach (var valute in valutes)
            {
                res = res + $"{valute.Name}:на дату: {valute.DateValute} --> {valute.Value}";
            }

            return res;
        }
    }
}
