using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riders.BL;
using Riders.Common.Model;

namespace Riders.PL.WPF
{
    internal class BetsViewModel
    {
        public Race Race { get; }
        public string BidderName { get; set; } = "";
        public decimal BidAmount { get; set; } = 100;

        public BetsViewModel(Race race)
        {
            Race = race;
        }
    }
}
