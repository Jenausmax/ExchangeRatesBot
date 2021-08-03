using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRatesBot.Domain.Models.GetModels
{
    public class Valute
    {
        public string Name { get; set; }
        public string CharCode { get; set; }
        public double Value { get; set; }
        public DateTime DateValute { get; set; }
    }
}
