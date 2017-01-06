using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riders.Common.Model;

namespace Riders.BL
{
    public class RaceResults
    {
        public Dictionary<Race, Rider> RaceWinners { get; private set; } = new Dictionary<Race, Rider>();
        public Dictionary<string, decimal> BiddersPrizes { get; private set; } = new Dictionary<string, decimal>();
    }
}
