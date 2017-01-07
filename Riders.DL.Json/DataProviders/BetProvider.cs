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
    internal class BetProvider : JsonDataProvider<Bet>
    {
        public BetProvider(string homeDirectory, DataContext dataContext) : base(homeDirectory, dataContext)
        {
        }

        protected override Bet FromJson(dynamic json)
        {
            long raceId = json.RaceId;
            long riderId = json.RiderId;

            return new Bet()
            {
                Amount = (decimal)json.Amount,
                BidderName = json.BidderName,
                RaceId = raceId,
                Race = Context.Races.Query.First(race => race.Id == raceId),
                RiderId = riderId,
                Rider = Context.Riders.Query.First(rider => rider.Id == riderId)
            };
        }

        protected override dynamic ToJson(Bet bet)
        {
            dynamic json = new ExpandoObject();

            json.Amount = bet.Amount;
            json.BidderName = bet.BidderName;
            json.RaceId = bet.Race?.Id ?? bet.RaceId;
            json.RiderId = bet.Rider?.Id ?? bet.RiderId;

            return json;
        }
    }
}
