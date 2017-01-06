using Riders.Common.Model;

namespace Riders.Common
{
    public abstract class DataContext
    {
        public abstract DataProvider<Horse> Horses { get; }
        public abstract DataProvider<Rider> Riders { get; }
        public abstract DataProvider<Race> Races { get; }
        public abstract DataProvider<Bet> Bets { get; }
    }
}
