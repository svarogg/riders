using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riders.Common;
using Riders.Common.Model;
using Riders.DL.Json.DataProviders;

namespace Riders.DL.Json
{
    public class JsonDataContext : DataContext
    {
        public JsonDataContext(string homeDirectory)
        {
            Horses = new HorseProvider(homeDirectory, this);
            Riders = new RiderProvider(homeDirectory, this);
            Races = new RaceProvider(homeDirectory, this);
            Bets = new BetProvider(homeDirectory, this);
        }

        public override DataProvider<Horse> Horses { get; }
        public override DataProvider<Rider> Riders { get; }
        public override DataProvider<Race> Races { get; }
        public override DataProvider<Bet> Bets { get; }
    }
}
