using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riders.Common;
using Riders.Common.Model;

namespace Riders.DL.Json.DataProviders
{
    internal class RaceProvider: JsonDataProvider<Race>
    {
        public RaceProvider(string homeDirectory, DataContext dataContext) : base(homeDirectory, dataContext)
        {
        }

        protected override Race FromJson(dynamic json)
        {
            long rider1Id = json.Rider1Id;
            long rider2Id = json.Rider2Id;

            return new Race
            {
                Rider1Id = rider1Id,
                Rider1 = Context.Riders.Query.First(rider => rider.Id == rider1Id),
                Rider2Id = rider2Id,
                Rider2 = Context.Riders.Query.First(rider => rider.Id == rider2Id)
            };
        }

        protected override dynamic ToJson(Race race)
        {
            dynamic json = new ExpandoObject();

            json.Rider1Id = race.Rider1?.Id ?? race.Rider1Id;
            json.Rider2Id = race.Rider2?.Id ?? race.Rider2Id;

            return json;
        }
    }
}
