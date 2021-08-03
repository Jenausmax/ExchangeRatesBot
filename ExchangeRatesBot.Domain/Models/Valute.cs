using System;
using System.Collections.Generic;

namespace ExchangeRatesBot.Domain.Models
{
    public class Valute
    {
        public DateTime DateGet { get; set; }
        public List<GetValuteModel> GetValuteModels { get; set; }
    }

    public sealed class GetValuteModel
    {
        public string Name { get; set; }
        public string CharCode { get; set; }
        public double Value { get; set; }
        public DateTime DateSave { get; set; }
        public DateTime DateValute { get; set; }
    }
}
