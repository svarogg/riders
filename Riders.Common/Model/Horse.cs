namespace Riders.Common.Model
{
    public class Horse : IIdentifieable
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public int Speed { get; set; }
        public int Endurance { get; set; }
        public int Health { get; set; }
    }
}
