namespace Riders.Common.Model
{
    public class Rider : IIdentifieable
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public int Control { get; set; }
        public int Fitness { get; set; }
        public int Weight { get; set; }

        public long HorseId { get; set; }
        public Horse Horse { get; set; }
    }
}
