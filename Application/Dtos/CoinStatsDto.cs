using System;

namespace CoinTree.Application.Dtos
{
    public class CoinStatsDto
    {
        public string Sell { get; set; }

        public string Buy { get; set; }

        public decimal Ask { get; set; }

        public decimal Bid { get; set; }

        public decimal Rate { get; set; }

        public decimal SpotRate { get; set; }

        public string Market { get; set; }

        public DateTime TimeStamp { get; set; }

        public string RateType { get; set; }
    }
}
