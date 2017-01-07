using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riders.Common;
using Riders.Common.Model;

namespace Riders.BL
{
    public static class DataGenerator
    {
        private const int ObjectsToGenerate = 5;
        private static readonly Random r = new Random();

        public static void GenerateData()
        {
            var horses = GenerateHorses();
            var riders = GenerateRiders(horses);
            var races = GenerateRaces(riders);
        }

        private static IList<Race> GenerateRaces(IList<Rider> riders)
        {
            var races = new List<Race>();
            for (var i = 0; i < ObjectsToGenerate; i++)
            {
                races.Add(GenerateRace(riders));
            }
            return races;
        }

        private static Race GenerateRace(IList<Rider> riders)
        {
            var ridersShuffled = riders.OrderBy(rider => r.Next());
            var rider1 = ridersShuffled.First();
            var rider2 = ridersShuffled.Skip(1).First();
            var race = new Race()
            {
                Rider1 = rider1,
                Rider2 = rider2,
            };

            return DataContextManager.Current.Races.SaveOrUpdate(race);
        }

        private static readonly List<string> riderFirstNames = new List<string> { "Hillary", "Bernie", "Ted", "Donald", "Vermin" };
        private static readonly List<string> riderLastNames = new List<string> { "Clinton", "Sanders", "Cruz", "Trump", "Supreme" };

        private static IList<Rider> GenerateRiders(IList<Horse> horses)
        {
            var riders = new List<Rider>();
            for (var i = 0; i < ObjectsToGenerate; i++)
            {
                riders.Add(GenerateRider(horses));
            }
            return riders;
        }

        private static Rider GenerateRider(IList<Horse> horses)
        {
            var horse = horses[r.Next(horses.Count)];
            var rider = new Rider()
            {
                Control = r.Next(100),
                Fitness = r.Next(100),
                Weight = r.Next(40, 81),
                Name = GenerateName(riderFirstNames, riderLastNames),
                Horse = horse
            };
            return DataContextManager.Current.Riders.SaveOrUpdate(rider);
        }

        private static readonly List<string> horseFirstNames = new List<string> { "Binky", "Horsy", "Soos", "Mister", "Rapidash" };
        private static readonly List<string> horseLastNames = new List<string> { "McHorseFace", "The Kid", "Thunder-Hoof", "De Lombard", "Snake Eyes" };

        private static IList<Horse> GenerateHorses()
        {
            var horses = new List<Horse>();
            for (var i = 0; i < ObjectsToGenerate; i++)
            {
                horses.Add(GenerateHorse());
            }
            return horses;
        }

        private static Horse GenerateHorse()
        {
            var horse = new Horse()
            {
                Endurance = r.Next(100),
                Speed = r.Next(100),
                Timidness = r.Next(100),
                Name = GenerateName(horseFirstNames, horseLastNames)
            };
            return DataContextManager.Current.Horses.SaveOrUpdate(horse);
        }

        private static string GenerateName(List<string> firstNames, List<string> lastNames)
        {
            var firstName = firstNames[r.Next(firstNames.Count)];
            var lastName = lastNames[r.Next(lastNames.Count)];

            return $"{firstName} {lastName}";
        }
    }
}
