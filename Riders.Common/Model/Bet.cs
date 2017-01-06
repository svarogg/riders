namespace Riders.Common.Model
{
    public class Bet :IIdentifieable
    {
        public long? Id { get; set; }

        public string BidderName { get; set; }
        public decimal Amount { get; set; }

        public long RaceId { get; set; }
        public Race Race { get; set; }

        public long RiderId { get; set; }
        public Rider Rider { get; set; }
    }
}
