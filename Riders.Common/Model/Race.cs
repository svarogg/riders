namespace Riders.Common.Model
{
    public class Race: IIdentifieable
    {
        public long? Id { get; set; }
        public long Rider1Id { get; set; }
        public virtual Rider Rider1 { get; set; }

        public long Rider2Id { get; set; }
        public virtual Rider Rider2 { get; set; }
    }
}
