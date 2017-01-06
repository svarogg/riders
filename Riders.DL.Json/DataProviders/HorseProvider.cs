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
    internal class HorseProvider : JsonDataProvider<Horse>
    {
        public HorseProvider(string homeDirectory, DataContext dataContext) : base(homeDirectory, dataContext)
        {
        }

        protected override Horse FromJson(dynamic json)
        {
            return new Horse()
            {
                Name = json.Name,
                Endurance = json.Endurance,
                Speed = json.Speed,
                Timidness = json.Timidness
            };
        }

        protected override dynamic ToJson(Horse horse)
        {
            dynamic json = new ExpandoObject();

            json.Name = horse.Name;
            json.Endurance = horse.Endurance;
            json.Speed = horse.Speed;
            json.Timidness = horse.Timidness;

            return json;
        }
    }
}
