using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riders.Common;
using Riders.Common.Model;

namespace Riders.BL
{
    public class DataGenerator
    {
        private const int ObjectsToGenerate = 5;
        private Random r = new Random();

        public void GenerateData()
        {
            var horses = GenerateHorses();
            var riders = GenerateRiders(horses);
            GenerateRaces(riders);
        }

        private IEnumerable<Race> GenerateRaces(IEnumerable<Rider> riders)
        {
            var ridersList = riders.ToList();
            for (var i = 0; i < ObjectsToGenerate; i++)
            {
                yield return GenerateRace(ridersList);
            }
        }

        private Race GenerateRace(List<Rider> ridersList)
        {
            var ridersShuffled = ridersList.OrderBy(rider => r.Next());
            var race = new Race()
            {
                Rider1 = ridersShuffled.First(),
                Rider2 = ridersShuffled.Skip(1).First()
            };

            return DataContext.Current.Races.SaveOrUpdate(race);
        }

        private readonly List<string> riderFirstNames = new List<string> { "Hillary", "Bernie", "Ted", "Donald", "Vermin" };
        private readonly List<string> riderLastNames = new List<string> { "Clinton", "Sanders", "Cruz", "Trump", "Supreme" };

        private IEnumerable<Rider> GenerateRiders(IEnumerable<Horse> horses)
        {
            var horsesList = horses.ToList();
            for (var i = 0; i < ObjectsToGenerate; i++)
            {
                yield return GenerateRider(horsesList);
            }
        }

        private Rider GenerateRider(IList<Horse> horses)
        {
            var rider = new Rider()
            {
                Control = r.Next(100),
                Fitness = r.Next(100),
                Weight = r.Next(40, 81),
                Name = GenerateName(riderFirstNames, riderLastNames),
                Horse = horses[r.Next(horses.Count)]
            };
            return DataContext.Current.Riders.SaveOrUpdate(rider);
        }

        private readonly List<string> horseFirstNames = new List<string> { "Binky", "Horsy", "Soos", "Mister", "Rapidash" };
        private readonly List<string> horseLastNames = new List<string> { "McHorseFace", "The Kid", "Thunder-Hoof", "De Lombard", "Snake Eyes" };

        private IEnumerable<Horse> GenerateHorses()
        {
            for (var i = 0; i < ObjectsToGenerate; i++)
            {
                yield return GenerateHorse();
            }
        }

        private Horse GenerateHorse()
        {
            var horse = new Horse()
            {
                Endurance = r.Next(100),
                Speed = r.Next(100),
                Timidness = r.Next(100),
                Name = GenerateName(horseFirstNames, horseLastNames)
            };
            return DataContext.Current.Horses.SaveOrUpdate(horse);
        }

        private string GenerateName(List<string> firstNames, List<string> lastNames)
        {
            var firstName = firstNames[r.Next(firstNames.Count)];
            var lastName = lastNames[r.Next(lastNames.Count)];

            return $"{firstName} {lastName}";
        }
    }
}
