using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riders.Common;
using Riders.Common.Model;

namespace Riders.DL.EF
{
    public class EfDataContext : DataContext
    {
        private static RidersContext Context { get; } = new RidersContext();

        public override DataProvider<Horse> Horses { get; } = new EfDataProvider<Horse>(Context, Context.Horses);
        public override DataProvider<Rider> Riders { get; }= new EfDataProvider<Rider>(Context, Context.Riders);
        public override DataProvider<Race> Races { get; }= new EfDataProvider<Race>(Context, Context.Races);
        public override DataProvider<Bet> Bets { get; }= new EfDataProvider<Bet>(Context, Context.Bets);
    }
}
