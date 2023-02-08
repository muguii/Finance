namespace Finance.Core.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdate { get; protected set; }

        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            LastUpdate = CreatedAt;
        }
    }
}
