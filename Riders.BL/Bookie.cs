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
            return DataContextManager.Current.Races.Query.ToList();
        }

        public static void PlaceBet(Race race, Rider rider, string bidderName, decimal amount)
        {
            var bet = new Bet
            {
                Race = race,
                Rider = rider,
                Amount = amount,
                BidderName = bidderName
            };
            DataContextManager.Current.Bets.SaveOrUpdate(bet);
        }

        public static RaceResults CommenceRaces()
        {
            var results = new RaceResults();

            foreach (var race in DataContextManager.Current.Races.Query.ToList())
            {
                var winner = race.GetWinner();
                results.RaceWinners.Add(race, winner);

                var winningBids =
                    DataContextManager.Current.Bets.Query
                    .Where(bet => bet.RaceId == race.Id && bet.RiderId == winner.Id)
                    .ToList();

                var oddsMultiplier = GetOddsMultiplier(race, winner);

                foreach (var winningBid in winningBids)
                {
                    if (!results.Prizes.ContainsKey(winningBid.BidderName))
                        results.Prizes[winningBid.BidderName] = 0;

                    results.Prizes[winningBid.BidderName] += Math.Round(winningBid.Amount * (decimal)oddsMultiplier + winningBid.Amount, 2);
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

            return (double) looserOdds/winnerOdds;
        }
    }
}
