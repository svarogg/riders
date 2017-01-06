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
    internal class RiderProvider:JsonDataProvider<Rider>
    {
        public RiderProvider(string homeDirectory, DataContext dataContext) : base(homeDirectory, dataContext)
        {
        }

        protected override Rider FromJson(dynamic json)
        {
            long horseId = json.horseId;

            return new Rider()
            {
                Name = json.Name,
                Control = json.Control,
                Fitness = json.Fitness,
                Weight = json.Weight,
                HorseId = horseId,
                Horse = Context.Horses.Queryable.First(horse => horse.Id == horseId)
            };
        }

        protected override dynamic ToJson(Rider rider)
        {
            dynamic json = new ExpandoObject();

            json.Name = rider.Name;
            json.Control = rider.Control;
            json.Fitness = rider.Fitness;
            json.Weight = rider.Weight;
            json.HorseId = rider.HorseId;

            return json;
        }
    }
}
