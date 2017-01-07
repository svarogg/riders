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
            long horseId = json.HorseId;

            return new Rider()
            {
                Name = json.Name,
                Control = (int)json.Control,
                Fitness = (int)json.Fitness,
                Weight = (int)json.Weight,
                HorseId = horseId,
                Horse = Context.Horses.Query.First(horse => horse.Id == horseId)
            };
        }

        protected override dynamic ToJson(Rider rider)
        {
            dynamic json = new ExpandoObject();

            json.Name = rider.Name;
            json.Control = rider.Control;
            json.Fitness = rider.Fitness;
            json.Weight = rider.Weight;
            json.HorseId = rider.Horse?.Id ?? rider.HorseId;

            return json;
        }
    }
}
