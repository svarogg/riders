using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Riders.BL;
using Riders.BL.ExtensionMethods;
using Riders.Common;
using Riders.Common.Model;

namespace Riders.PL.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var races = Bookie.GetRaces();
            if (!races.Any())
            {
                GenerateAndPlay();
            }
            else
            {
                Play(races);
            }
            Console.ReadLine();
        }

        private static void PrintIntro()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("Welcome To HorseBeto-Interactivo 2000++ !");
            Console.WriteLine("=========================================");
        }

        private static void Play(IList<Race> races)
        {
            PlaceBets(races);
            var results = Bookie.CommenceRaces();
            PrintResults(results);
        }

        private static void PrintResults(RaceResults results)
        {
            Console.Clear();
            Console.WriteLine("The games are over!");
            Console.WriteLine("And here are the results!");
            Console.WriteLine();

            foreach (var gameResult in results.RaceWinners)
            {
                var race = gameResult.Key;
                var winner = gameResult.Value;
                Console.WriteLine($"Race between {race.Rider1.Name} and {race.Rider2.Name}. Winner: {winner.Name}");
            }
            Console.WriteLine();
            Console.WriteLine("Prizes go to:");

            foreach (var prize in results.Prizes)
            {
                var bidderName = prize.Key;
                var amount = prize.Value;
                Console.WriteLine($"{bidderName} won ${amount}!");
            }
        }

        private static void PlaceBets(IList<Race> races)
        {
            while (true)
            {
                Console.Clear();
                PrintIntro();
                PrintTodayRaces(races);

                Console.WriteLine();
                Console.WriteLine("Please press the number of the game you wish to bet on, or 0 to begin games");

                var selection = Console.ReadLine();
                if (selection == "0")
                    return;

                int raceNumber;
                if (Int32.TryParse(selection, out raceNumber) && raceNumber > 0 && raceNumber < races.Count + 1)
                {
                    SelectRider(races[raceNumber - 1]);
                }
                else
                {
                    Console.WriteLine("Bad selection! Please try again!");
                }
            }
        }

        private static void SelectRider(Race race)
        {
            Console.Clear();
            Console.WriteLine("You are betting on game:");
            PrintRider(race.Rider1);
            Console.WriteLine();
            Console.WriteLine("vs");
            Console.WriteLine();
            PrintRider(race.Rider2);

            Console.WriteLine($"Press 1 to bet on {race.Rider1.Name}");
            Console.WriteLine($"Press 2 to bet on {race.Rider2.Name}");
            Console.WriteLine("Press 0 or anything else to cancel bet");

            switch (Console.ReadLine())
            {
                case "1":
                    PlaceBet(race, race.Rider1);
                    break;
                case "2":
                    PlaceBet(race, race.Rider2);
                    break;
                default:
                    Console.WriteLine("Cancelling bet");
                    break;
            }
        }

        private static void PlaceBet(Race race, Rider rider)
        {
            Console.WriteLine("Please input your name");
            var bidderName = Console.ReadLine();

            Console.WriteLine("Please input the bet amount");
            var amount = Decimal.Parse(Console.ReadLine() ?? "");

            Bookie.PlaceBet(race, rider, bidderName, amount);
        }

        private static void PrintRider(Rider rider)
        {
            Console.WriteLine($"Rider: {rider.Name}");
            Console.WriteLine($"\tControl: {rider.Control} ");
            Console.WriteLine($"\tFitness: {rider.Fitness} ");
            Console.WriteLine($"\tWeight: {rider.Weight} ");

            var horse = rider.Horse;
            Console.WriteLine($"Riding: {horse.Name}");
            Console.WriteLine($"\tSpeed: {horse.Speed} ");
            Console.WriteLine($"\tEndurance: {horse.Endurance} ");
            Console.WriteLine($"\tTimidness: {horse.Health} ");

            Console.WriteLine();
            Console.WriteLine($"Total Score: {rider.GetAvarageScore()}");
        }

        private static void PrintTodayRaces(IList<Race> races)
        {
            Console.WriteLine("Today's races:");

            for (var i = 0; i < races.Count; i++)
            {
                var race = races[i];
                var odds = race.GetOdds();
                Console.WriteLine(
                    $"[{i + 1}] {race.Rider1.Name} riding {race.Rider1.Horse.Name} vs {race.Rider2.Name} riding {race.Rider2.Horse.Name} (odds {odds.Item1}:{odds.Item2})");
            }
        }

        private static void GenerateAndPlay()
        {
            DataGenerator.GenerateData();
            Play(Bookie.GetRaces());
        }
    }
}
