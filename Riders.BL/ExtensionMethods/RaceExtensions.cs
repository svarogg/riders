using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riders.Common.Model;

namespace Riders.BL.ExtensionMethods
{
    public static class RaceExtensions
    {
        public static Tuple<int, int> GetOdds(this Race race)
        {
            return new Tuple<int, int>(race.Rider1.GetAvarageScore(), race.Rider2.GetAvarageScore());
        }

        public static Rider GetWinner(this Race race)
        {
            Rider winner = null;
            do
            {
                var rider1Score = race.Rider1.GetRaceScore();
                var rider2Score = race.Rider2.GetRaceScore();

                if (rider1Score > rider2Score)
                    winner = race.Rider1;
                else if (rider1Score < rider2Score)
                    winner = race.Rider2;
            } while (winner == null);

            return winner;
        }
    }
}
