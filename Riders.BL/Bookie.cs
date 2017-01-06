using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Riders.BL.ExtensionMethods;
using Riders.Common;
using Riders.Common.Model;

namespace Riders.BL
{
    public static class Bookie
    {
        public static IList<Race> GetRaces()
        {
            return DataContext.Current.Races.Queryable.ToList();
        }

        public static void PlaceBet(Race race, Rider rider, string bidderName, decimal amount)
        {
            var bet = new Bet
            {
                Race = race,
                Rider = rider
            };
            DataContext.Current.Bets.SaveOrUpdate(bet);
        }

        public static RaceResults CommenceRaces()
        {
            var results = new RaceResults();

            foreach (var race in DataContext.Current.Races.Queryable)
            {
                var winner = race.GetWinner();
                results.RaceWinners.Add(race, winner);

                var winningBids =
                    DataContext.Current.Bets.Queryable.Where(bet => bet.Race == race && bet.Rider == winner);

                var oddsMultiplier = GetOddsMultiplier(race, winner);

                foreach (var winningBid in winningBids)
                {
                    if (results.BiddersPrizes.ContainsKey(winningBid.BidderName))
                        results.BiddersPrizes[winningBid.BidderName] += Math.Round(winningBid.Amount*(decimal) oddsMultiplier, 2);
                }
            }

            return results;
        }

        private static double GetOddsMultiplier(Race race, Rider winner)
        {
            var winnerOdds = winner.GetAvarageScore();
            var looser = race.Rider1 == winner
                ? race.Rider2
                : race.Rider1;
            var looserOdds = looser.GetAvarageScore();

            return (double)winnerOdds / looserOdds;
        }
    }
}
